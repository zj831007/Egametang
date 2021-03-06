﻿using Model;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotfix
{
	public abstract class Component : Disposer
	{
		[BsonId]
		[BsonIgnore]
		public sealed override long Id { get; set; }

		[BsonIgnore]
		public Entity Entity { get; set; }

		public T GetEntity<T>() where T : Entity
		{
			return this.Entity as T;
		}

		protected Component()
		{
			this.Id = IdGenerater.GenerateId();
		}

		public T GetComponent<T>() where T : Component
		{
			return this.Entity.GetComponent<T>();
		}

		public override void Dispose()
		{
			if (this.Id == 0)
			{
				return;
			}

			base.Dispose();

			this.Entity?.RemoveComponent(this.GetType());
		}
	}
}