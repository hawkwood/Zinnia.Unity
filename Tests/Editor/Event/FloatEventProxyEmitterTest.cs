﻿using Zinnia.Event;

namespace Test.Zinnia.Event
{
    using UnityEngine;
    using NUnit.Framework;
    using Test.Zinnia.Utility.Mock;

    public class FloatEventProxyEmitterTest
    {
        private GameObject containingObject;
        private FloatEventProxyEmitter subject;

        [SetUp]
        public void SetUp()
        {
            containingObject = new GameObject();
            subject = containingObject.AddComponent<FloatEventProxyEmitter>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(subject);
            Object.DestroyImmediate(containingObject);
        }

        [Test]
        public void Receive()
        {
            UnityEventListenerMock emittedMock = new UnityEventListenerMock();
            subject.Emitted.AddListener(emittedMock.Listen);
            float payload = 10f;

            Assert.IsFalse(emittedMock.Received);
            subject.Receive(payload);
            Assert.AreEqual(payload, subject.Payload);
            Assert.IsTrue(emittedMock.Received);
        }

        [Test]
        public void ReceiveInactiveGameObject()
        {
            UnityEventListenerMock emittedMock = new UnityEventListenerMock();
            subject.Emitted.AddListener(emittedMock.Listen);
            subject.gameObject.SetActive(false);

            float payload = 10f;

            Assert.IsFalse(emittedMock.Received);
            subject.Receive(payload);
            Assert.AreEqual(payload, subject.Payload);
            Assert.IsFalse(emittedMock.Received);
        }

        [Test]
        public void ReceiveInactiveComponent()
        {
            UnityEventListenerMock emittedMock = new UnityEventListenerMock();
            subject.Emitted.AddListener(emittedMock.Listen);
            subject.enabled = false;

            float payload = 10f;

            Assert.IsFalse(emittedMock.Received);
            subject.Receive(payload);
            Assert.AreEqual(payload, subject.Payload);
            Assert.IsFalse(emittedMock.Received);
        }
    }
}
