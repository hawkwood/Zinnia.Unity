﻿using Zinnia.Event;

namespace Test.Zinnia.Event
{
    using UnityEngine;
    using NUnit.Framework;
    using Test.Zinnia.Utility.Mock;

    public class ComponentGameObjectEmitterTest
    {
        private GameObject containingObject;
        private ComponentGameObjectEmitter subject;

        [SetUp]
        public void SetUp()
        {
            containingObject = new GameObject();
            subject = containingObject.AddComponent<ComponentGameObjectEmitter>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(subject);
            Object.DestroyImmediate(containingObject);
        }

        [Test]
        public void Extract()
        {
            GameObject source = new GameObject();

            UnityEventListenerMock extractedMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedMock.Listen);

            subject.SetSource(source.transform);

            Assert.IsFalse(extractedMock.Received);
            subject.Extract();
            Assert.IsTrue(extractedMock.Received);
            Assert.AreEqual(subject.Result, source);

            Object.DestroyImmediate(source);
        }

        [Test]
        public void ExtractInvalidSource()
        {
            GameObject source = new GameObject();

            UnityEventListenerMock extractedMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedMock.Listen);

            Assert.IsFalse(extractedMock.Received);
            subject.Extract();
            Assert.IsFalse(extractedMock.Received);
            Assert.IsNull(subject.Result);

            Object.DestroyImmediate(source);
        }
    }
}