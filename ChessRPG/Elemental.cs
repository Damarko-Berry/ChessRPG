using System;
using System.Collections.Generic;
using System.Text;

namespace ChessRPG
{
    internal struct Elemental
    {
        public Element Weakness, Stronger;
        public Elemental(Element Strong_Against, Element WeakAgainst)
        {
            Weakness = WeakAgainst;
            Stronger = Strong_Against;
        }

        public Relationship GetRelation(Element Off) => (Off == Stronger) ? Relationship.Stronger : (Off == Weakness) ? Relationship.Weakness : Relationship.Nuetural;
    }

    internal static class ElementalJudge
    {
        public static Dictionary<Element, Elemental> EJ = new Dictionary<Element, Elemental>()
        {
            { Element.Fire, new Elemental(Element.Wind,Element.Water)},
            { Element.Water, new Elemental(Element.Fire, Element.Earth)},
            { Element.Lightning, new Elemental(Element.Earth,Element.Wind)},
            { Element.Wind, new Elemental(Element.Lightning,Element.Fire)},
            { Element.Earth, new Elemental(Element.Water,Element.Lightning)},
        };
    }
}
