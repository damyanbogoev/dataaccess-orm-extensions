Meet Telerik DataAccess Extensions
==================================

## Context Extensions ##

The project provides a fluent API for configuring a new OpenAccessContext instance. The entry point of the API is the Db.Init() method. It allows developers to specify a connection string and setup the backend configuration settings, metadata, fetch strategy and cache key for the underlying Database instance.

### Connection String ###

At the moment there are two approaches for specifying a connection string using the context extensions fluent API.

Specifying a raw connection string:

	Db.Init()
      .ConnectionString(x => x.Set("Server=BOGOEV; Initial Catalog=Northwind; Integrated Security=True"))

or looking for connection string under the configuration files based on its connection id:

	Db.Init()
      .ConnectionString(x => x.LoadFromConfig("NorthwindConnection"))

The later method will look for a connection string with the *NorthwindConnection* **name** under the **connectionStrings** section or for application setting with *NorthwindConnection* **key** under the *appSettings section*.

	<connectionStrings>
	  <add name="NorthwindConnection" connectionString="Data Source=BOGOEV;Initial Catalog=Northwind;Integrated Security=True;"/>
	</connectionStrings>

or

	<appSettings>
	  <add key="NorthwindConnection" value="Data Source=BOGOEV;Initial Catalog=Northwind;Integrated Security=True;"/>
	</appSettings>

### Backend Configuration Settings ###


At the moment there are two approaches for specifying backend configuration settings using the context extensions fluent API.

Loading the backend configuration settings from the configuration file. More information about this approach can be found [here](http://docs.telerik.com/data-access/developers-guide/data-access-domain-model/managing-the-openaccesscontext/data-access-tasks-model-tools-configure-backend-object).

	Db.Init()
      .BackendConfiguration(b => b.LoadFromConfig("MssqlConfiguration", ConfigurationMergeMode.ConfigFileDefinitionWins))

or

	Db.Init()
	  .BackendConfiguration(b =>
                {
                    b.ConnectionPool(cp => cp.ActiveConnectionTimeout = 120);
                    b.Logging(l => l.LogEvents = LoggingLevel.All);
                })

The later overload allows developers to set the specific settings for the connection pool, logging, runtime, etc. using the provided backend configurator methods.

### Metadata ###

The metadata container can be passed to the context fluent API using the Metadata method. It allows to pass a metadata container instance:

	Db.Init()
	  .MetadataContainer(x => x.Container(containerInstance))

To specify a metadata source:

	Db.Init()
		.MetadataContainer(m => m.Source<NorthwindMetadataSource>())

or

	Db.Init()
		.MetadataContainer(m => m.Source(metadataSourceInstance))

Very helpful extension of the metadata container API is the NullForeignKey method, which specifies that foreign key constraints should be created during the schema migration process.

	Db.Init()
	  .MetadataContainer(m => m.Source<MyMetadataSource>().NullForeignKey())

### Fetch Strategy ###

To specify a fetch strategy for the OpenAccessContext being constructed, the FetchStrategy API should be used:

	Db.Init()
      .FetchStrategy(fs => fs.Set(instance))

### Database Cache Key ###

To specify a database cache key:

	Db.Init()
      .CacheKey("SampleKey")

### Build a new OpenAccessContext instance ###

To create a new OpenAccessContext instance based on the provided settings the Build method should be used:

	var context = Db.Init()
                .ConnectionString(c => c.LoadFromConfig("NorthwindConnection"))
                .BackendConfiguration(b => b.LoadFromConfig("MssqlConfiguration", ConfigurationMergeMode.ConfigFileDefinitionWins))
                .MetadataContainer(m => m.Source<NorthwindMetadataSource>().NullForeignKey())
                .CacheKey("SampleKey")
                .Build<NorthwindContext>(migrateSchema: true);

The Build method has a parameter which controls if we want to migrate the schema of the underlying database. Default value is set to **false**.


## DataAccessContext ##

DataAccessContext adds additional functionality to the OpenAccessContext.

### Validation ###

If an IDataAccessValidator is specified and the ShouldValidateEntities property is set to true all added and modified entities will be validate using the IDataAccessValidator before persisting the changes to the database.

An easy way to setup the validation for the DataAccessContext is to use the DataAcess ORM fluent API:

	Db.Init()
      .EnableValidation(new DataAnnotationsValidator())

The DataAnnotationsValidator validator is a built-in validation which uses data annotations to validate the entities.

It is very easy to implement a custom validator the uses the FluentValidation framework. Custom implementation can be found under the MayLily.DataAccess.ContextExtensions.Sample.

