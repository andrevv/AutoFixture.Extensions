# AutoFixture Extensions
[![Build Status](https://travis-ci.org/andrevv/AutoFixture.Extensions.svg?branch=master)](https://travis-ci.org/andrevv/AutoFixture.Extensions)
[![Build status](https://ci.appveyor.com/api/projects/status/bp2a0gu7ep7rb6af/branch/master?svg=true)](https://ci.appveyor.com/project/andrevv/autofixture-extensions/branch/master)

## Methods

### With<T, TProperty, TValue>()

* **T** - entity type
* **TProperty** - target property type
* **TValue** - source value type

Allows building entity using regular IFixture.With() method, but with different types of source value and destination property. A converter between specified types must be registered. There are some predefined converters, but if there is no appropriate one, it is possible to register a custom converter using Extend() method.

```csharp
public class Foo
{
  public string Value { get; set }
}

int value = ...;

var foo = new Fixture()
  .Build<Foo>()
  .With(x => x.Value, value)
  .Create();
```

### Json<T, TProperty, TValue>()

Converts parameter of type TValue to a property of type TProperty using JSON serialization. It uses With() extension method, and since JSON is basically a string, corresponding value converter between string type and TProperty type has to be registered. Although, if TProperty is a string type, no extra converter is needed.

```csharp
public class Foo { }
public class Bar
{
  public string Json { get; set; }
}

var foo = new Foo();

var bar = new Fixture()
  .Build<Bar>()
  .Json(x => x.Json, foo)
  .Create();
```

### Extend(this IFixture fixture, Type source, Type target, IValueConverter converter)

Registers converter between a source and target types.

```csharp
public class Foo { }
public class Bar { }

public class Baz
{
  public Bar Bar { get; set; }
}

var foo = new Foo();

var baz = new Fixture()
  .Extend(typeof(Foo), typeof(Bar), new FooBarConverter())
  .Build<Baz>()
  .With(x => x.Bar, foo)
  .Create();
```

## Classes

### ValueConverter

It is a base type for custom value converters. Use Extend() method to register custom value converter.

```csharp
public class Foo { }
public class Bar { }

public class FooBarConverter : ValueConverter<Foo, Bar>
{
  protected override Bar Convert(Foo value)
  {
    var bar = new Bar();
    
    // initialization logic
    
    return bar;
  }
}
```
