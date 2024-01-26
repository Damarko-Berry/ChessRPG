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
        public Piece King = new Piece(PieceType.King);
        public Piece Queen= new Piece(PieceType.Queen);
        public Piece[] Bishops = new Piece[2]
        {
            new Piece(PieceType.Bishop),
            new Piece(PieceType.Bishop,1)
        };
        public Piece[] Rooks = new Piece[2]
        {
            new Piece(PieceType.Rook),
            new Piece(PieceType.Rook,1)
        };
        public Piece[] Knights = new Piece[2]
        {
            new Piece(PieceType.Knight),
            new Piece(PieceType.Knight, 1)
        }; 
        public Piece[] Pawns = new Piece[8]
        {
            new Piece(PieceType.Pawn,0),
            new Piece(PieceType.Pawn,1),
            new Piece(PieceType.Pawn,2),
            new Piece(PieceType.Pawn,3),
            new Piece(PieceType.Pawn,4),
            new Piece(PieceType.Pawn,5),
            new Piece(PieceType.Pawn,6),
            new Piece(PieceType.Pawn,7),
        };

        public Team(){}
        public Team(int lvl)
        {
            exp = lvl * 49;
            Random random = new Random();
            King.AddExp(exp);
            Queen.AddExp(random.Next(exp));
            for (int i = 0; i < Bishops.Length; i++)
            {
                random = new Random();
                Bishops[i].AddExp(random.Next(exp));
            }
            for (int i = 0; i < Rooks.Length; i++)
            {
                random = new Random();
                Rooks[i].AddExp(random.Next(exp));
            }
            for (int i = 0; i < Knights.Length; i++)
            {
                random = new Random();
                Knights[i].AddExp(random.Next(exp));
            }
            for (int i = 0; i < Pawns.Length; i++)
            {
                random = new Random();
                Pawns[i].AddExp(random.Next(exp));
            }
            race = (Race)random.Next(Enum.GetNames(typeof(Race)).Length);
        }
        public Team(string path)
        {
            if (File.Exists(path))
            {
                var team = SaveLoad.Load<Team>(path);
                King = team.King;
                Queen = team.Queen;
                Bishops = team.Bishops;
                Rooks = team.Rooks;
                Knights = team.Knights;
                Pawns = team.Pawns;
            } 
        }
    }
}
