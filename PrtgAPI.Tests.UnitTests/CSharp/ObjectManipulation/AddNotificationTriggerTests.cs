﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrtgAPI.Parameters;
using PrtgAPI.Tests.UnitTests.Support;
using PrtgAPI.Tests.UnitTests.Support.TestResponses;
using PrtgAPI.Tests.UnitTests.Support.TestItems;

namespace PrtgAPI.Tests.UnitTests.ObjectManipulation
{
    [TestClass]
    public class AddNotificationTriggerTests : BaseTest
    {
        [TestMethod]
        public void AddNotificationTrigger_SupportedType()
        {
            var client = Initialize_Client(new SetNotificationTriggerResponse());

            var parameters = new ThresholdTriggerParameters(1001)
            {
                Channel = new TriggerChannel(1)
            };

            client.AddNotificationTrigger(parameters, false);
        }

        [TestMethod]
        public async Task AddNotificationTrigger_SupportedTypeAsync()
        {
            var client = Initialize_Client(new SetNotificationTriggerResponse());

            var parameters = new ThresholdTriggerParameters(1001)
            {
                Channel = new TriggerChannel(1)
            };

            await client.AddNotificationTriggerAsync(parameters, false);
        }

        [TestMethod]
        public void AddNotificationTrigger_UnsupportedType()
        {
            var client = Initialize_Client(new SetNotificationTriggerResponse());

            var parameters = new StateTriggerParameters(1001);

            AssertEx.Throws<InvalidTriggerTypeException>(() => client.AddNotificationTrigger(parameters), "Trigger type 'State' is not a valid trigger type");
        }

        [TestMethod]
        public async Task AddNotificationTrigger_UnsupportedTypeAsync()
        {
            var client = Initialize_Client(new SetNotificationTriggerResponse());

            var parameters = new StateTriggerParameters(1001);

            await AssertEx.ThrowsAsync<InvalidTriggerTypeException>(async() => await client.AddNotificationTriggerAsync(parameters), "Trigger type 'State' is not a valid trigger type");
        }

        [TestMethod]
        public void AddNotificationTrigger_ChannelToContainer()
        {
            var dictionary = new Dictionary<Content, int>
            {
                [Content.Sensors] = 0
            };

            var client = Initialize_Client(new SetNotificationTriggerResponse(dictionary));

            var parameters = new ThresholdTriggerParameters(1001)
            {
                Channel = new TriggerChannel(1)
            };

            AssertEx.Throws<InvalidOperationException>(() => client.AddNotificationTrigger(parameters), "Channel ID '1' is not a valid channel for Device, Group or Probe");
        }

        [TestMethod]
        public async Task AddNotificationTrigger_ChannelToContainerAsync()
        {
            var dictionary = new Dictionary<Content, int>
            {
                [Content.Sensors] = 0
            };

            var client = Initialize_Client(new SetNotificationTriggerResponse(dictionary));

            var parameters = new ThresholdTriggerParameters(1001)
            {
                Channel = new TriggerChannel(1)
            };

            await AssertEx.ThrowsAsync<InvalidOperationException>(async () => await client.AddNotificationTriggerAsync(parameters), "Channel ID '1' is not a valid channel for Device, Group or Probe");
        }

        [TestMethod]
        public void AddNotificationTrigger_EnumToSensor()
        {
            var client = Initialize_Client(new SetNotificationTriggerResponse());

            var parameters = new ThresholdTriggerParameters(1001);

            AssertEx.Throws<InvalidOperationException>(() => client.AddNotificationTrigger(parameters), "Channel 'Primary' is not a valid value for sensor");
        }

        [TestMethod]
        public async Task AddNotificationTrigger_EnumToSensorAsync()
        {
            var client = Initialize_Client(new SetNotificationTriggerResponse());

            var parameters = new ThresholdTriggerParameters(1001);

            await AssertEx.ThrowsAsync<InvalidOperationException>(async () => await client.AddNotificationTriggerAsync(parameters), "Channel 'Primary' is not a valid value for sensor");
        }

        [TestMethod]
        public void AddNotificationTrigger_ResolveScenarios()
        {
            var client = Initialize_Client(new DiffBasedResolveResponse(false));

            var parameters = new StateTriggerParameters(1001)
            {
                OnNotificationAction = {Id = 301}
            };

            var resolvedTrigger = client.AddNotificationTrigger(parameters);

            Assert.AreEqual("Email to all members of group PRTG Users Group 2", resolvedTrigger.OnNotificationAction.ToString());

            var trigger = client.AddNotificationTrigger(parameters, false);
            Assert.AreEqual(null, trigger, "Trigger was not null");
        }

        [TestMethod]
        public async Task AddNotificationTrigger_ResolveScenariosAsync()
        {
            var client = Initialize_Client(new DiffBasedResolveResponse(false));

            var parameters = new StateTriggerParameters(1001)
            {
                OnNotificationAction = { Id = 301 }
            };

            var resolvedTrigger = await client.AddNotificationTriggerAsync(parameters);

            Assert.AreEqual("Email to all members of group PRTG Users Group 2", resolvedTrigger.OnNotificationAction.ToString());

            var trigger = await client.AddNotificationTriggerAsync(parameters, false);
            Assert.AreEqual(null, trigger, "Trigger was not null");
        }

        [TestMethod]
        public void AddNotificationTrigger_Throws_ResolvingMultiple()
        {
            var client = Initialize_Client(new DiffBasedResolveResponse(true));

            var parameters = new StateTriggerParameters(1001)
            {
                OnNotificationAction = { Id = 301 }
            };

            var str = "Could not uniquely identify created NotificationTrigger: multiple new objects ('Type = State, Inherited = False, OnNotificationAction = Email to all members of group PRTG Users Group 2',";

            AssertEx.Throws<ObjectResolutionException>(() => client.AddNotificationTrigger(parameters), str);
        }

        [TestMethod]
        public async Task AddNotificationTrigger_Throws_ResolvingMultipleAsync()
        {
            var client = Initialize_Client(new DiffBasedResolveResponse(true));

            var parameters = new StateTriggerParameters(1001)
            {
                OnNotificationAction = { Id = 301 }
            };

            var str = "Could not uniquely identify created NotificationTrigger: multiple new objects ('Type = State, Inherited = False, OnNotificationAction = Email to all members of group PRTG Users Group 2',";

            await AssertEx.ThrowsAsync<ObjectResolutionException>(async () => await client.AddNotificationTriggerAsync(parameters), str);
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_StandardTriggerChannel_OnSensor()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678", //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns)                   //Validate TriggerChannel target compatibility
            };

            AssertEx.Throws<InvalidOperationException>(() => TestTriggerChannel(TriggerChannel.Total, urls, true), "Channel 'Total' is not a valid value");
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_StandardTriggerChannel_OnContainer()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678", //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns), //Validate TriggerChannel target compatibility
                "https://prtg.example.com/editsettings?onnotificationid_=-1%7cNone&class=threshold&offnotificationid_new=-1%7cNone&channel_new=-1&condition_new=0&threshold_new=0&latency_new=60&id=1001&subid=new&objecttype=nodetrigger&username=username&passhash=12345678", //Add Trigger
            };

            TestTriggerChannel(TriggerChannel.Total, urls, false);
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_Channel_OnSensor()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678",                                                   //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns),                                                                    //Validate TriggerChannel target compatibility
                "https://prtg.example.com/api/table.xml?content=channels&columns=objid,name,lastvalue&count=*&id=1001&username=username&passhash=12345678",
                "https://prtg.example.com/controls/channeledit.htm?id=1001&channel=1&username=username&passhash=12345678",
                "https://prtg.example.com/editsettings?onnotificationid_=-1%7cNone&class=threshold&offnotificationid_new=-1%7cNone&channel_new=1&condition_new=0&threshold_new=0&latency_new=60&id=1001&subid=new&objecttype=nodetrigger&username=username&passhash=12345678", //Add Trigger
            };

            var channel = new Channel
            {
                Name = "Percent Available Memory",
                Id = 1
            };

            TestTriggerChannel(channel, urls, true);
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_Channel_OnContainer()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678",                                                   //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns),                                                                    //Validate TriggerChannel target compatibility
            };

            var channel = new Channel
            {
                Name = "Percent Available Memory",
                Id = 1
            };

            AssertEx.Throws<InvalidOperationException>(() => TestTriggerChannel(channel, urls, false), "Channel 'Percent Available Memory' of type 'Channel' is not a valid channel");
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_ChannelId_OnSensor()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678",                                                   //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns),                                                                    //Validate TriggerChannel target compatibility
                "https://prtg.example.com/controls/channeledit.htm?id=1001&channel=3&username=username&passhash=12345678",
                "https://prtg.example.com/editsettings?onnotificationid_=-1%7cNone&class=threshold&offnotificationid_new=-1%7cNone&channel_new=3&condition_new=0&threshold_new=0&latency_new=60&id=1001&subid=new&objecttype=nodetrigger&username=username&passhash=12345678", //Add Trigger
            };

            TestTriggerChannel(new TriggerChannel(3), urls, true);
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_ChannelId_OnContainer()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678",                                                   //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns)                                                                     //Validate TriggerChannel target compatibility
            };

            AssertEx.Throws<InvalidOperationException>(() => TestTriggerChannel(new TriggerChannel(3), urls, false), "Channel ID '3' is not a valid channel");
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_InvalidChannelId_OnSensor()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678",                                                   //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns),                                                                    //Validate TriggerChannel target compatibility
                "https://prtg.example.com/controls/channeledit.htm?id=1001&channel=99&username=username&passhash=12345678"
            };

            AssertEx.Throws<InvalidOperationException>(() => TestTriggerChannel(new TriggerChannel(99), urls, true), "Channel ID '99' is not a valid channel");
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_InvalidChannelId_OnContainer()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678",                                                   //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns)                                                                     //Validate TriggerChannel target compatibility
            };

            AssertEx.Throws<InvalidOperationException>(() => TestTriggerChannel(new TriggerChannel(99), urls, false), "Channel ID '99' is not a valid channel");
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_Channel_WithStandardTriggerChannelName_OnSensor()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678",                                                   //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns),                                                                    //Validate TriggerChannel target compatibility
                "https://prtg.example.com/api/table.xml?content=channels&columns=objid,name,lastvalue&count=*&id=1001&username=username&passhash=12345678",
                "https://prtg.example.com/controls/channeledit.htm?id=1001&channel=1&username=username&passhash=12345678",
                "https://prtg.example.com/editsettings?onnotificationid_=-1%7cNone&class=threshold&offnotificationid_new=-1%7cNone&channel_new=1&condition_new=0&threshold_new=0&latency_new=60&id=1001&subid=new&objecttype=nodetrigger&username=username&passhash=12345678", //Add Trigger
            };

            var channel = new Channel
            {
                Name = "Total",
                Id = 1
            };

            TestTriggerChannel(channel, urls, true, new ChannelItem(name: "Total", objId: "1"));
        }

        [TestMethod]
        public void AddNotificationTrigger_TriggerChannel_Channel_WithStandardTriggerChannelName_OnContainer()
        {
            var urls = new[]
            {
                "https://prtg.example.com/api/triggers.json?id=1001&username=username&passhash=12345678",                                                   //Validate Supported Triggers
                TestHelpers.RequestSensor("count=*&filter_objid=1001", UrlFlag.Columns)                                                                     //Validate TriggerChannel target compatibility
            };

            var channel = new Channel
            {
                Name = "Total"
            };

            AssertEx.Throws<InvalidOperationException>(() => TestTriggerChannel(channel, urls, false), "Channel 'Total' of type 'Channel' is not a valid channel");
        }

        private void TestTriggerChannel(TriggerChannel channel, string[] urls, bool isSensor, ChannelItem channelItem = null)
        {
            var client = Initialize_Client(new AddressValidatorResponse(urls)
            {
                CountOverride = new Dictionary<Content, int>
                {
                    [Content.Sensors] = isSensor ? 2 : 0
                },
                ItemOverride = channelItem != null ? new Dictionary<Content, BaseItem[]>
                {
                    [Content.Channels] = new[] {channelItem}
                } : null
            });

            var parameters = new ThresholdTriggerParameters(1001)
            {
                Channel = channel
            };

            client.AddNotificationTrigger(parameters, false);
        }
    }
}
