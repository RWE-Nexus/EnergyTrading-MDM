***
*** NOTE:  As of 13/12/2013 the T4 templates are probably not going to generate the classes to the correct locations as
***		   things have been moved around slightly.  Discussion to be had regarding whether (and how) we maintain the T4 going forward
***


*** NOTE: this scripting process is fine tuned for generation of non temporal entities. Some bits will need to change for temporal entities

Pre-Reqs:

Install the T4 Toolbox VS extension - http://visualstudiogallery.msdn.microsoft.com/7f9bd62f-2505-4aa4-9378-ee7830371684

Create an MDM shell database using Red Gate SQL Compare


NEW ENTITY
----------
To create a new entity. Make a new folder _YourEntity and copy over the scripts:

1_CreateModelScript.tt
2_CreateCodeScript.tt
3_CreateUnitTestScript.tt
4_CreateRestServices.tt
5_CreateDataAndCheckerScript.tt
6_CreateIntegrationTestScript.tt
AuditTableScript.tt
MetaData.tt

To run scripts the custom tool needs to be set in the properties window to: TextTemplatingFileGenerator 
When the tool is set and the file is saved the t4 template will run.

*** BEFORE Generating code, turning off SVN integration in Visual Studio, and re-enable afterwards, otherwise we get delete/created files rather than modified files ***

Copy the codegen folder _NonTemporalEntity and rename as _YourEntity

Now run the scripts in the following order:

Uncomment the contents of the 1_CreateModelScript.tt, and edit to include the name of the new entity

NB. The guts of this template file need to be moved to MDM.Contracts.sln, as the Contract is no longer contained in MDM.sln
Now Run 1_CreateModelScript.tt
	if(PartyRole) then remove the x.gen class and just leave the entity class. Delete everything excpet for the copydetails methods. See exchange for an example
	if(PartyRole) then add the inheritance relationship to the new detail and entity record - see Exchange for an example
	if(PartyRole) then remove everything from the details class except for the new properties that the entity has. See Exchange for example

### For new objects add the files Entity.cs, Entity.gen.cs, EntityMapping.cs generated under Mdm.Core to the Mdm.Core project #####

Make sure everything compiles	
Search for any TODO_ModelGeneration's - complete all tasks
Make sure everything compiles


### Now Add your objects and properties into the Mdm.Contracts solution so that they can be found in the next stage ####	
	

Edit MetaData.tt (see example at bottom of document for more info)

Run 2_CreateCodeScript.tt (creates all new code classes excluding REST services)
	Check everything builds
	Search for any TODO_CodeGeneration's - complete all tasks except...
		if(PartyRole) then don't add configuration in MappingContext. This is all done in PartyRole configuration classes, make sure you update PartyRoleConfiguration and PartyRoleDetailsConfiguration

#######	NOTE: If you are adding a new entity make sure that you add ALL entries for both the entity and the entity mapping where necessary (e.g. in MappingContext.cs) #####


	Check everything builds

Generate AuditScripts to create audit tables and triggers
> Navigate to MetaData.tt and change the tuple parameters to include server and table names
> Save AuditTableScript.tt or right click - run custom tool
> It will create audit scripts in Code\MDM.CodeGeneration\Database\AuditScripts
> open sql files and execute against the desired db
> remove these sql file reference from code generation project, because we don't check-in sql scripts in subversion


Run 3_CreateUnitTestScript.tt
	Check everything builds
	Search for any TODO_UnitTestGeneration's - complete all tasks
	Check everything builds
	Run all unit tests and make sure they pass

Run 4_CreateRestServices.tt
	Check everything builds
	Search for any TODO_RestServiceGeneration's - complete all tasks except...

Run 5_CreateDataAndCheckerScript.tt
Run 6_CreateIntegrationTestScript.tt
	Search for any TODO_IntegrationTest's - complete all tasks
	Check all integration tests run


UPDATE EXISTING ENTITIES
------------------------

A. Adding properties
-------------------------------
Adding properties needs to be done carefully since we need to be able to rollback a software version from production
if it turns out that it has a critical bug.

The full procedure for how to amend and publish the schema for this is documented at (), but the short form is that
changes should be promoted to production as soon as possible.

One the schema changes are done, the following files need to be updated...

Core
----
Details Mapper
Contracts\Details Mapper
Validator

Core.Test
---------
Checker
Builder

Data
----
Configuration


B. Amending properties
----------------------

C. Deleting properties


META DATA EXAMPLES
------------------

Entity Name, Is Temporal, MDM Details class, Contract Details class

Temporal Example:

	var entityNames = new List<Tuple<string, bool, Type, Type>>()
    {
        Tuple.Create("Person", true, typeof(RWEST.Nexus.MDM.PersonDetails), typeof(RWEST.Nexus.MDM.Contracts.PersonDetails))
    };

Non Temporal Example:

	var entityNames = new List<Tuple<string, bool, Type, Type>>()
    {
        Tuple.Create("Market", false, typeof(RWEST.Nexus.MDM.Market), typeof(RWEST.Nexus.MDM.Contracts.MarketDetails))
    };

More than one entity could be added to this file, but it works better if you create one entity only.