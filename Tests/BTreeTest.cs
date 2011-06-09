using System;
using NUnit.Framework;
using BPlusTree;

namespace Tests
{
    [TestFixture]
    public class BTreeTest
    {
        [Test]
        public void CanSearchForValueInTree()
        {
            var tree = new BTree<string>(5);
            tree.Insert("/usr0/actors/DanielCraig");
            tree.Insert("/usr0/writers/AlistairMacLean");
            tree.Insert("/usr0/superheroes/Xavier");
            tree.Insert("/usr0/heroes/Vendetta");
            tree.Insert("/usr0/actors/HumphryBogart");
            tree.Insert("/usr0/scientists/MichaelFaraday");
            tree.Insert("/usr0/actors/Armorer");
            tree.Insert("/usr0/philosophers/Aristotle");
            tree.Insert("/usr0/superheroes/BruceWayne");
            tree.Insert("/usr0/worlds/Hyrule");
            tree.Insert("/usr0/mmo/Ettenmoor");
            tree.Insert("/usr0/diseases/Dengue");
            tree.Insert("/usr0/listerature/Jekyll");
            tree.Insert("/usr0/random/Koralos");
            tree.Insert("/usr0/scientists/Lagrange");
            tree.Insert("/usr0/random/Nurf");
            tree.Insert("/usr0/mammals/Orca");
            tree.Insert("/usr0/mythicals/Werewolf");
            tree.Insert("/usr0/places/Cheydinhal");
            tree.Insert("/usr0/compilers/ClangCompiler");
            tree.Insert("/usr0/places/DeepSea");
            tree.Insert("/usr0/places/Clyde");
            tree.Insert("/usr0/places/Clydesdale");

            Console.Out.WriteLine(tree);
            BTreeNodeElement<string> element = tree.Search("/usr0/places/Clyde");
            Console.Out.WriteLine("Found:" + element.Value);
        }
    }
}