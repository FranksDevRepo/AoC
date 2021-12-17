using aoc2021.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Packet Decoder")]
public sealed class Day16 : SolutionBase
{
    private string binaryString;
    private Packet Outer;
    public override string Part1(string input)
    {
        /*  D2FE28 becomes:
            1101 0010 1111 1110 0010 1000

            110100101111111000101000
            VVVTTTAAAAABBBBBCCCCC

            VVV = packet version 
            TTT = packet type : 4 = literal value
                   0111 1110 0101
        */
        binaryString = ConvertHexToBinary(RemoveNewLine(input));
        int increment = 0;
        Outer = GetNextPacket(binaryString, 0, ref increment);
        return Outer.VersionSum.ToString();

    }

    public override string Part2(string input)
    {
        binaryString = ConvertHexToBinary(RemoveNewLine(input));
        int increment = 0;
        Outer = GetNextPacket(binaryString, 0, ref increment);
        return Outer.Value.ToString();
    }

    private static string RemoveNewLine(string input)
        => input.Replace(Environment.NewLine, string.Empty).Replace("\n", string.Empty);

    private static string ConvertHexToBinary(string input) 
        => string.Join(string.Empty, input.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

    private Packet GetNextPacket(string binaryString, int startPoint, ref int incrementBy)
    {
        int tmpInc = 0;
        Packet packet = new();
        packet.Version = Convert.ToInt32(binaryString.Substring(startPoint, 3), 2);
        tmpInc += 3;
        packet.TypeID = Convert.ToInt32(binaryString.Substring(startPoint + tmpInc, 3), 2);
        tmpInc += 3;


        if (packet.TypeID == LITERAL_PACKET)
        {
            StringBuilder literalValue = new();

            while (binaryString[startPoint + tmpInc] == '1')
            {
                tmpInc++;
                literalValue.Append(binaryString.AsSpan(startPoint + tmpInc, 4));
                tmpInc += 4;
            }

            tmpInc++;
            literalValue.Append(binaryString.AsSpan(startPoint + tmpInc, 4));
            tmpInc += 4;
            packet.LiteralValue = Convert.ToInt64(literalValue.ToString(), 2);
        }
        else
        {
            int subTmpInc = 0;
            if (binaryString[startPoint + tmpInc] == LENGTH_TYPE_TOTAL_LENGTH) //Next 15 bits encode total length in bits
            {
                tmpInc++;
                int totalLengthOfSubs = Convert.ToInt32(binaryString.Substring(startPoint + tmpInc, 15), 2);
                tmpInc += 15;
                while (subTmpInc < totalLengthOfSubs)
                {
                    packet.SubPackets.Add(GetNextPacket(binaryString, startPoint + tmpInc + subTmpInc, ref subTmpInc));
                }
            }
            else //next 11 bits encode total number of subpackets
            {
                tmpInc++;
                int totalCountOfSubs = Convert.ToInt32(binaryString.Substring(startPoint + tmpInc, 11), 2);
                tmpInc += 11;
                foreach (int _ in Enumerable.Range(0, totalCountOfSubs))
                {
                    packet.SubPackets.Add(GetNextPacket(binaryString, startPoint + tmpInc + subTmpInc, ref subTmpInc));
                }
            }
            tmpInc += subTmpInc;
        }

        incrementBy += tmpInc;
        return packet;
    }

    private class Packet
    {
        public int TypeID { get; set; }
        public int Version { get; set; }
        public long LiteralValue { get; set; }
        public List<Packet> SubPackets = new();
        public long VersionSum => Version + SubPackets.Sum(x => x.VersionSum);
        public long Value => (TypeID) switch
        {
            0 => SubPackets.Sum(p => p.Value),
            1 => SubPackets.Aggregate(1L, (acc, val) => (acc * val.Value)),
            2 => SubPackets.Min(p => p.Value),
            3 => SubPackets.Max(p => p.Value),
            4 => LiteralValue,
            5 => SubPackets[0].Value > SubPackets[1].Value ? 1 : 0,
            6 => SubPackets[0].Value < SubPackets[1].Value ? 1 : 0,
            7 => SubPackets[0].Value == SubPackets[1].Value ? 1 : 0,
            _ => throw new NotImplementedException(),
        };
    }

    private const int LITERAL_PACKET = 4;
    private const char LENGTH_TYPE_TOTAL_LENGTH = '0';
    private const char LENGTH_TYPE_NUMBER_PACKETS = '1';
}