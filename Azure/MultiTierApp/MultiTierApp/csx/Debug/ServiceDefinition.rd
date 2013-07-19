<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="MultiTierApp" generation="1" functional="0" release="0" Id="fe25e06b-4cd7-4969-9768-1357e82d586d" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MultiTierAppGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="FrontendWebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MultiTierApp/MultiTierAppGroup/LB:FrontendWebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="FrontendWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MultiTierApp/MultiTierAppGroup/MapFrontendWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="FrontendWebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MultiTierApp/MultiTierAppGroup/MapFrontendWebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="OrderProcessingRole:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MultiTierApp/MultiTierAppGroup/MapOrderProcessingRole:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="OrderProcessingRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MultiTierApp/MultiTierAppGroup/MapOrderProcessingRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="OrderProcessingRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MultiTierApp/MultiTierAppGroup/MapOrderProcessingRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:FrontendWebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MultiTierApp/MultiTierAppGroup/FrontendWebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapFrontendWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MultiTierApp/MultiTierAppGroup/FrontendWebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapFrontendWebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MultiTierApp/MultiTierAppGroup/FrontendWebRoleInstances" />
          </setting>
        </map>
        <map name="MapOrderProcessingRole:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MultiTierApp/MultiTierAppGroup/OrderProcessingRole/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapOrderProcessingRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MultiTierApp/MultiTierAppGroup/OrderProcessingRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapOrderProcessingRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MultiTierApp/MultiTierAppGroup/OrderProcessingRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="FrontendWebRole" generation="1" functional="0" release="0" software="C:\Users\eric\github\DemoCode\Azure\MultiTierApp\MultiTierApp\csx\Debug\roles\FrontendWebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;FrontendWebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;FrontendWebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;OrderProcessingRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MultiTierApp/MultiTierAppGroup/FrontendWebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MultiTierApp/MultiTierAppGroup/FrontendWebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MultiTierApp/MultiTierAppGroup/FrontendWebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="OrderProcessingRole" generation="1" functional="0" release="0" software="C:\Users\eric\github\DemoCode\Azure\MultiTierApp\MultiTierApp\csx\Debug\roles\OrderProcessingRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;OrderProcessingRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;FrontendWebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;OrderProcessingRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MultiTierApp/MultiTierAppGroup/OrderProcessingRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MultiTierApp/MultiTierAppGroup/OrderProcessingRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MultiTierApp/MultiTierAppGroup/OrderProcessingRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="FrontendWebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="OrderProcessingRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="FrontendWebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="OrderProcessingRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="FrontendWebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="OrderProcessingRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="39b927b6-e3b3-43d3-899f-8e54ffc00ab0" ref="Microsoft.RedDog.Contract\ServiceContract\MultiTierAppContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="1ce43db5-69ff-4fb9-b05a-58da338c2c75" ref="Microsoft.RedDog.Contract\Interface\FrontendWebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MultiTierApp/MultiTierAppGroup/FrontendWebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>