﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1495
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		[Test]
		public async Task CreateTestAsync()
		{
			object id;

			using (ISession session = OpenSession())
			{
				var person = new Person {Name = "Nelo"};

				using (ITransaction trans = session.BeginTransaction())
				{
					await (session.SaveAsync(person));
					await (trans.CommitAsync());
				}

				id = person.Id;
			}

			using (ISession session = OpenSession())
			{
				var person = (IPerson)await (session.LoadAsync(typeof(Person), id)); //to work with the proxy

				Assert.IsNotNull(person);
				Assert.AreEqual("Nelo", person.Name);

				using (ITransaction trans = session.BeginTransaction())
				{
						await (session.DeleteAsync(person));
						await (trans.CommitAsync());
				}
			}
		}
	}
}
