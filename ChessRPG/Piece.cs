using System;
using System.ComponentModel.Design;
using System.Runtime.ExceptionServices;

namespace ChessRPG
{
    [Serializable]
    public class Piece
    {
        public int Level = 1;
        public int EXP;
        public int Worth
        {
            get
            {
                var t = 0;
                switch (pieceType)
                {
                    case PieceType.Pawn:
                        t = 1;
                        break;
                    case PieceType.Rook: 
                        t = 5;
                        break;
                    case PieceType.Knight:
                        t = 3;
                        break;
                    case PieceType.Bishop:
                        t = 3;
                        break;
                    case PieceType.Queen:
                        t = 9;
                        break;
                    case PieceType.King:
                        t = 10;
                        break;
                }
                return t*Level;
            }
        }
        public int index;
        public PieceType pieceType;
        public Element element;
        private Elemental ER => ElementalJudge.EJ[element];
        public Upgrades upgrades;
        public int atr;
        public Gender gender;
        public int MaxHP => (BaseStr+BaseSpd)*10;
        public int HP;
        public int Str => (BaseStr+upgrades.addedstr)*10;
        public int Spd => (BaseSpd+upgrades.addedspd)*10;
        public int Def => ((Str+Spd)/2);

        int def;
        int GetDef(Element off)
        {
            var D = def;
            if (Level < 5) return D;
            var Rela = ER.GetRelation(off);
            switch (Rela)
            {
                case Relationship.Weakness:
                    D *= 2;
                    break;
                case Relationship.Stronger: 
                    D /= 2;
                    break;
                 case Relationship.Nuetural:
                    break;
            }
            return D;
        }
        int BaseStr
        {
            get
            {
                int val = 0;
                switch (pieceType)
                {
                    case PieceType.Pawn:
                        val = 2;
                        break;
                    case PieceType.Rook:
                        val = 3;
                        break;
                    case PieceType.Bishop:
                        val = 2;
                        break;
                    case PieceType.Knight: 
                        val = 2;
                        break;
                    case PieceType.Queen:
                        val = 4;
                        break;
                    case PieceType.King:
                        val = 2;
                        break;
                }
                return val * Level;
            }
        }
        int BaseSpd
        {
            get
            {
                int val = 0;
                switch (pieceType)
                {
                    case PieceType.Pawn:
                        val = 1;
                        break;
                    case PieceType.Rook:
                        val = 2;
                        break;
                    case PieceType.Bishop:
                        val = 3;
                        break;
                    case PieceType.Knight:
                        val = 2;
                        break;
                    case PieceType.Queen:
                        val = 4;
                        break;
                    case PieceType.King:
                        val = 2;
                        break;
                }
                return val * Level;
            }
        }
        public void AddExp(int amt)
        {
           
            for (int i = 0; i < amt; i++)
            {
                EXP++;
                if (EXP >= 49*Level)
                {
                    Level++;
                    if (Level % 3 == 0)
                    {
                        atr++;
                    }
                }
            }
        }
        public Piece()
        {
            Random random = new Random();
            Level = 1;
            HP = MaxHP;
            element = (Element)random.Next(Enum.GetValues(typeof(Element)).Length);
        }
        public Piece(PieceType pieceType)
        {
            this.pieceType = pieceType;
            index = 0;
            Level = 1;
            HP = MaxHP;
            Random random = new Random();
            element = (Element)random.Next(Enum.GetValues(typeof(Element)).Length);
            if(pieceType == PieceType.King)
            {
                gender = Gender.Male;
            }
            else if(pieceType  == PieceType.Queen)
            {
                gender = Gender.Female;
            }
            else
            {
                gender = (Gender)random.Next(2);
            }
        } 
        public Piece(PieceType pieceType, int index)
        {
            this.pieceType = pieceType;
            this.index = index;
            Level = 1;
            HP = MaxHP;
            Random random = new Random();
            element = (Element)random.Next(Enum.GetValues(typeof(Element)).Length);
            if (pieceType == PieceType.King)
            {
                gender = Gender.Male;
            }
            else if (pieceType == PieceType.Queen)
            {
                gender = Gender.Female;
            }
            else
            {
                gender = (Gender)random.Next(2);
            }
        }
        public int takeAttack(Piece attacker, bool inRange)
        {
            if (!inRange) return 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int atk = random.Next(attacker.Str);
            int ED = 0;
            if (attacker.Level > 5)
            {   
                ED = atk / 2;
                if (Level > 5) 
                { 
                    switch (ER.GetRelation(attacker.element))
                    {
                        case Relationship.Stronger:
                            ED = 0;
                            break;
                        case Relationship.Weakness:
                            ED += (ED/2);
                            break;
                        default:
                            break;
                    }
                }   
            }
            var D = (attacker.Level > 5) ? GetDef(attacker.element) : def;
            if (random.Next(attacker.Spd) >= D)
            {
                ResetDef();
            }
            else
            {
                atk=0; ED=0;
                def -= random.Next(attacker.Str);
            }
            HP -= atk;
            return atk+ED;
        }
        public void ResetDef() {  def = Def; }
    }
}
