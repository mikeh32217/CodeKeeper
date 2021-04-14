
Currently in Version 1.0 of the Configuration MAnager I have provided 2 types of providers;
	1) KeyValueProvider that is a ReadOnly provider that as the name imples has a key and a value.
		The KeyValueProvider is defined in the RourceSection in the listing that follows.
		In the listing below the provider is used as a dictionary for common strings I use in my applications.
		Looking at the file you will notice there are "[% xxx %]" scattered around, these are replacement
		tokens.  See the Translate methods in the printout below for an explanation.
	2) KeySubKeyProvider is used where multiple subkeys are defined for a single key.  These values
		may be updated or added dynamically/prgrammatically.  The KeyValueProvider is defined in the 
		ApplicationSection in the listing that follows.

One of the things that makes this manager unique is not only that value can be changed, as in the case
of the KeySubKeyProvider but also that Configuration files may be spread out in separate files and 
merged into manager or you may have multiple sections in the same config file.  In the source provided
with this utitlity there are two config files CommonResources.xml and testConfif.xml.

The config file is broke up into two main sections the ConfigSection where the user sections are defined 
and the user sections.  To configure a user section and typical entry in ConfigSection is shown in the
listing that follows and the attributes are;
	
	SectionName - The name of the user section.  
	Name		- Name that you will use to access in your code.
	Provider	- Currently there are only two providers available
					1) KeyValueProvider
					2) KeySubKeyProvider

	<?xml version="1.0" encoding="utf-8"?>
	<Configuration>
	  <ConfigSection>
		<Section SectionName="ResourceSection" Name="Msgs" Provider="KeyValueProvider" />
		<Section SectionName="ApplicationSection" Name="Settings" Provider="KeySubkeyProvider" />
	  </ConfigSection>
	  <ResourceSection>
	    <add key="Name" value="Mighty Mouse"/>
	    <add key="SuperHero" value="My hero is [% Name %]"/>
		<add key="FileNotFound" value="Could not find the file [% file %]" />
		<add key="DeleteConfirm" value="You are about to delete [% file %]\n Do you wish to continue?" />
		<add key="XmlParseError" value="There was an error while parsing the Xml file [% file %]" />
		<add key="XmlSerializeError" value="An error occured while attempting to serialize the [% obj %] object\n" />
		<add key="SaveProjectError" value="An error occured while truing to save the Simulator project file [% file %]" />
		<add key="AbortMsg" value="The current action was aborted by the user" />
		<add key="SaveConfirm" value="The file [% file %] was saved successfully" />
		<add key="NewProjectConfirm" value="You will lose any unsaved changes if you continue, proceed?" />
	  </ResourceSection>
	  <ApplicationSection>
		<ScreenCoords xpos="100" ypos="100" />
		<IsVisible visible="true" />
		<GradientBrush color1="-32640" color2="-32640" angle="100.0" />
		<CedarKey pastime="fishing" export="fish" import="yakees" />
	  </ApplicationSection>
	</Configuration>

The listing below shows examples of how to use the configuration manager.

        //Instantiate the Configuration manager
        CustomConfigurationManager configMgr = new CustomConfigurationManager();

        //Load the Configuration files
        configMgr.Initialize(Environment.CurrentDirectory + "\\" + "CommonResources.xml");
        configMgr.Initialize(Environment.CurrentDirectory + "\\" + "TestConfig.xml");

        //Get a reference to the provider using the name provided in the section.
        KeyValueProvider msgProvider = (KeyValueProvider)configMgr.GetProvider("Msgs");
        KeySubkeyProvider appProvider = (KeySubkeyProvider)configMgr.GetProvider("Settings");
        KeyValueProvider testProvider = (KeyValueProvider)configMgr.GetProvider("Test");
            
        //The KeyValueProvider has 2 methods to Translate a string and replace the [% xxx %] token 
        //  with one of two possible values.  The KeyValueProvider is ReadOnly.
        //      1) The following replaces the token with the string in parameter 2
        string str = testProvider.Translate("MyName", "Mike Hankey");
        //      2) And this one that replaces the value with another value in the dictionary.
        string str1 = testProvider.Translate("SuperHero");

        //The KeySubKeyProvider allows one key with many subkeys and is Writabke. NOT ReadOnly
        //Get all the value associated with the key
        Dictionary<string, string> strs = appProvider.GetValues("GradientBrush");

        //Get a single value from the dictionary using key and subkey
        str = appProvider.GetSingleValue("GradientBrush", "angle");

        str = string.Empty;
        //Values may be updated or add to the Dictionary using SetValue method
        //If the value exists in the dictionary it is updated and it is added
        //  to the dictionary if it isn't there already.
        appProvider.SetValue("CedarKey", "pastime", "fishing");
        appProvider.SetValue("CedarKey", "export", "fish");
        appProvider.SetValue("CedarKey", "import", "yakees");

        //This is the way to commit all changes to the config file.  All changes are
        //made to the in-memory dictionary but not committed until the SaveConfigChanges
        //is called.
        configMgr.SaveConfigChanges();
