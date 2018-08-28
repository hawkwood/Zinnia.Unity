﻿using VRTK.Core.Tracking.Collision;
using VRTK.Core.Tracking.Collision.Active.Operation;

namespace Test.VRTK.Core.Tracking.Collision.Active.Operation
{
    using UnityEngine;
    using NUnit.Framework;
    using Test.VRTK.Core.Utility.Mock;

    public class NotifierContainerExtractorTest
    {
        private GameObject containingObject;
        private NotifierContainerExtractor subject;

        [SetUp]
        public void SetUp()
        {
            containingObject = new GameObject();
            subject = containingObject.AddComponent<NotifierContainerExtractor>();
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
            UnityEventListenerMock extractedMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedMock.Listen);

            GameObject forwardSource = new GameObject();
            CollisionNotifier.EventData notifier = new CollisionNotifier.EventData();
            notifier.forwardSource = forwardSource.GetComponent<Component>();

            Assert.IsNull(subject.ForwardSourceContainer);
            Assert.IsFalse(extractedMock.Received);

            subject.Extract(notifier);

            Assert.AreEqual(forwardSource, subject.ForwardSourceContainer);
            Assert.IsTrue(extractedMock.Received);

            Object.DestroyImmediate(forwardSource);
        }

        [Test]
        public void ExtractInactiveGameObject()
        {
            UnityEventListenerMock extractedMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedMock.Listen);

            GameObject forwardSource = new GameObject();
            CollisionNotifier.EventData notifier = new CollisionNotifier.EventData();
            notifier.forwardSource = forwardSource.GetComponent<Component>();

            Assert.IsNull(subject.ForwardSourceContainer);
            Assert.IsFalse(extractedMock.Received);

            subject.gameObject.SetActive(false);
            subject.Extract(notifier);

            Assert.IsNull(subject.ForwardSourceContainer);
            Assert.IsFalse(extractedMock.Received);

            Object.DestroyImmediate(forwardSource);
        }

        [Test]
        public void ExtractInactiveComponent()
        {
            UnityEventListenerMock extractedMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedMock.Listen);

            GameObject forwardSource = new GameObject();
            CollisionNotifier.EventData notifier = new CollisionNotifier.EventData();
            notifier.forwardSource = forwardSource.GetComponent<Component>();

            Assert.IsNull(subject.ForwardSourceContainer);
            Assert.IsFalse(extractedMock.Received);

            subject.enabled = false;
            subject.Extract(notifier);

            Assert.IsNull(subject.ForwardSourceContainer);
            Assert.IsFalse(extractedMock.Received);

            Object.DestroyImmediate(forwardSource);
        }

        [Test]
        public void ExtractInvalidNotifier()
        {
            UnityEventListenerMock extractedMock = new UnityEventListenerMock();
            subject.Extracted.AddListener(extractedMock.Listen);

            GameObject forwardSource = new GameObject();
            CollisionNotifier.EventData notifier = null;

            Assert.IsNull(subject.ForwardSourceContainer);
            Assert.IsFalse(extractedMock.Received);

            subject.Extract(notifier);

            Assert.IsNull(subject.ForwardSourceContainer);
            Assert.IsFalse(extractedMock.Received);

            Object.DestroyImmediate(forwardSource);
        }
    }
}