using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChessRPG
{
    [Serializable]
    public class Team
    {
        public Race race;
        int exp;
        public void AddExp(int amt)
        {
            exp = amt;
        }
        public void Distribute()
        {
            King.AddExp(exp);
            Queen.AddExp(exp);
            for (int i = 0; i < Bishops.Length; i++)
            {
                Bishops[i].AddExp(exp);
            }
            for (int i = 0; i < Rooks.Length; i++)
            {
                Rooks[i].AddExp(exp);
            }
            for (int i = 0; i < Knights.Length; i++)
            {
                Knights[i].AddExp(exp);
            }
            for (int i = 0; i < Pawns.Length; i++)
            {
                Pawns[i].AddExp(exp);
            }
            exp = 0;
        }
        public Piece King;
        public Piece Queen;
        public Piece[] Bishops,Rooks, Knights, Pawns;
        public Piece[] all
        {
            get
            {
                List<Piece> list = new List<Piece>();
                list.Add(King);
                list.Add(Queen);
                list.AddRange(Bishops);
                list.AddRange(Rooks);
                list.AddRange(Knights);
                list.AddRange(Pawns);
                return list.ToArray();
            }
        }
        public TeamStatistics Statistics => new TeamStatistics(all);
        Team(){}
        public void Regen(Statistics statistics)
        {
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].HP <= 0)
                {
                    all[i] = new Piece(all[i].pieceType,statistics);
                }
            }
        }
        public Team(Race rce, Statistics statistics)
        {
            race = rce;
            King = new Piece(PieceType.King,statistics);
            Queen = new Piece(PieceType.Queen, statistics);
            Bishops = new Piece[2]
            {
                new Piece(PieceType.Bishop,statistics),
                new Piece(PieceType.Bishop,1,statistics),
            };
            Rooks = new Piece[2]{
                new Piece(PieceType.Rook,statistics),
                new Piece(PieceType.Rook,1,statistics),
            };
            Knights = new Piece[2]{
                new Piece(PieceType.Knight,statistics),
                new Piece(PieceType.Knight,1,statistics),
            };
            Pawns = new Piece[8]{
                new Piece(PieceType.Pawn,statistics),
                new Piece(PieceType.Pawn,1,statistics),
                new Piece(PieceType.Pawn,2,statistics),
                new Piece(PieceType.Pawn,3,statistics),
                new Piece(PieceType.Pawn,4,statistics),
                new Piece(PieceType.Pawn,5,statistics),
                new Piece(PieceType.Pawn,6,statistics),
                new Piece(PieceType.Pawn,7,statistics),
            };
            
        }

    }

    public readonly struct TeamStatistics
    {
        public readonly Stat Str;
        public readonly Stat Spd;
        public readonly Stat Def;
        public readonly Stat Lvl;
        public TeamStatistics(Piece[] piece)
        {
            Str = new Stat();
            Spd = new Stat();
            Def = new Stat();
            Lvl = new Stat();
            for (int i = 0; i < piece.Length; i++)
            {
                Str.Add(piece[i].Str);
                Spd.Add(piece[i].Spd);
                Def.Add(piece[i].Def);
                Lvl.Add(piece[i].Level);
            }
        }
    }
}
