﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using NUnit.Framework;
using NHibernate.Linq;
using System.Linq;
using NHibernate.Linq.Functions;

namespace NHibernate.Test.NHSpecificTest.NH2394
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		protected override void OnTearDown()
		{
			using (ISession s = Sfi.OpenSession())
			{
				s.Delete("from A");
				s.Flush();
			}
		}

		[Test]
		public async Task LinqUserTypeEqualityAsync()
		{
			ISession s = OpenSession();
			try
			{
				await (s.SaveAsync(new A { Type = TypeOfA.Awesome, Phone = new PhoneNumber(1, "555-1111") }));
				await (s.SaveAsync(new A { Type = TypeOfA.Boring, NullableType = TypeOfA.Awesome, Phone = new PhoneNumber(1, "555-2222") }));
				await (s.SaveAsync(new A { Type = TypeOfA.Cool, Phone = new PhoneNumber(1, "555-3333") }));
				await (s.FlushAsync());
			}
			finally
			{
				s.Close();
			}

			s = OpenSession();
			try
			{
				A item;

				Assert.AreEqual(3, (await (s.CreateQuery("from A a where a.IsNice = ?").SetParameter(0, false).ListAsync())).Count);
				Assert.AreEqual(3, await (s.Query<A>().CountAsync(a => a.IsNice == false)));

				item = await (s.CreateQuery("from A a where a.Type = ?").SetParameter(0, TypeOfA.Awesome).UniqueResultAsync<A>());
				Assert.AreEqual(TypeOfA.Awesome, item.Type);
				Assert.AreEqual("555-1111", item.Phone.Number);

				item = await (s.Query<A>().Where(a => a.Type == TypeOfA.Awesome).SingleAsync());
				Assert.AreEqual(TypeOfA.Awesome, item.Type);
				Assert.AreEqual("555-1111", item.Phone.Number);

				item = await (s.Query<A>().Where(a => TypeOfA.Awesome == a.Type).SingleAsync());
				Assert.AreEqual(TypeOfA.Awesome, item.Type);
				Assert.AreEqual("555-1111", item.Phone.Number);

				IA interfaceItem = await (s.Query<IA>().Where(a => a.Type == TypeOfA.Awesome).SingleAsync());
				Assert.AreEqual(TypeOfA.Awesome, interfaceItem.Type);
				Assert.AreEqual("555-1111", interfaceItem.Phone.Number);

				item = await (s.CreateQuery("from A a where a.NullableType = ?").SetParameter(0, TypeOfA.Awesome).UniqueResultAsync<A>());
				Assert.AreEqual(TypeOfA.Boring, item.Type);
				Assert.AreEqual("555-2222", item.Phone.Number);
				Assert.AreEqual(TypeOfA.Awesome, item.NullableType);

				item = await (s.Query<A>().Where(a => a.NullableType == TypeOfA.Awesome).SingleAsync());
				Assert.AreEqual(TypeOfA.Boring, item.Type);
				Assert.AreEqual("555-2222", item.Phone.Number);
				Assert.AreEqual(TypeOfA.Awesome, item.NullableType);

				Assert.AreEqual(2, await (s.Query<A>().CountAsync(a => a.NullableType == null)));

				item = await (s.CreateQuery("from A a where a.Phone = ?").SetParameter(0, new PhoneNumber(1, "555-2222")).UniqueResultAsync<A>());
				Assert.AreEqual(TypeOfA.Boring, item.Type);
				Assert.AreEqual("555-2222", item.Phone.Number);

				item = await (s.Query<A>().Where(a => a.Phone == new PhoneNumber(1, "555-2222")).SingleAsync());
				Assert.AreEqual(TypeOfA.Boring, item.Type);
				Assert.AreEqual("555-2222", item.Phone.Number);
			}
			finally
			{
				s.Close();
			}
		}
	}
}
