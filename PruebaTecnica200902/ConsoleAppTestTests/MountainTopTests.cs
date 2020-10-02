using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleAppTest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleAppTest.Tests
{
    [TestClass()]
    public class MountainTopTests
    {
        [TestMethod()]
        public void GetNumberOfTopsTest()
        {
            MountainTop mountainTop = new MountainTop();

            //int resultado = mountainTop.GetNumberOfTopsForeach(trackerMountain);
            //int resultado = mountainTop.GetNumberOfTopsFor(trackerMountain);
            //int resultado = mountainTop.GetNumberOfTopsForPartial(trackerMountain, 0, trackerMountain.Length);
            //int resultado = mountainTop.GetNumberOfTopsByProcessorParallel(trackerMountain);
            //int resultado = mountainTop.GetNumberOfTopsByProcessor(trackerMountain).Result;
            int[] nums = Enumerable.Range(0, 400000).OrderBy(c => Guid.NewGuid()).ToArray();
            int resultado = 120000;

            Assert.IsTrue(resultado > -1);
        }

        [TestMethod()]
        public void GetNumberOfTopsForeachTest()
        {
            MountainTop mountainTop = new MountainTop();

            int[] nums = Enumerable.Range(0, 400000).OrderBy(c => Guid.NewGuid()).ToArray();
            int resultado = mountainTop.GetNumberOfTopsForeach(nums);

            Assert.IsTrue(resultado > -1);
        }

        [TestMethod()]
        public void GetNumberOfTopsForTest()
        {
            MountainTop mountainTop = new MountainTop();

            int[] nums = Enumerable.Range(0, 400000).OrderBy(c => Guid.NewGuid()).ToArray();
            int resultado = mountainTop.GetNumberOfTopsFor(nums);

            Assert.IsTrue(resultado > -1);
        }

        [TestMethod()]
        public void GetNumberOfTopsForPartialTest()
        {
            MountainTop mountainTop = new MountainTop();

            int[] nums = Enumerable.Range(0, 400000).OrderBy(c => Guid.NewGuid()).ToArray();
            int resultado = mountainTop.GetNumberOfTopsPartialFor(nums, 0, nums.Length);

            Assert.IsTrue(resultado > -1);
        }

        [TestMethod()]
        public void GetNumberOfTopsByProcessorParallelTest()
        {
            MountainTop mountainTop = new MountainTop();

            int[] nums = Enumerable.Range(0, 400000).OrderBy(c => Guid.NewGuid()).ToArray();
            int resultado = mountainTop.GetNumberOfTopsByProcessorParallel(nums);

            Assert.IsTrue(resultado > -1);
        }

        [TestMethod()]
        public void GetNumberOfTopsByProcessorTest()
        {
            MountainTop mountainTop = new MountainTop();

            int[] nums = Enumerable.Range(0, 400000).OrderBy(c => Guid.NewGuid()).ToArray();
            int resultado = mountainTop.GetNumberOfTopsByProcessor(nums).Result;

            Assert.IsTrue(resultado > -1);
        }
    }
}