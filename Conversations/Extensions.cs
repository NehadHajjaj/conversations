namespace Conversations
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal static class Extensions
	{
		public static void EnumerableNavigationProperty<T>(
			this EntityTypeBuilder<T> entity,
			string propertyName,
			string fieldName) where T : class
		{
			var childrenProperty = entity.Metadata.FindNavigation(propertyName);
			childrenProperty.SetPropertyAccessMode(PropertyAccessMode.Field);
			childrenProperty.SetField(fieldName);
		}

		public static void EnforceMaxLength(this string value, int maxLength)
		{
			if (maxLength < 0)
			{
				throw new ArgumentException("Max length cannot be less than zero.", nameof(maxLength));
			}

			if (value?.Length > maxLength)
			{
				throw new ArgumentException($"Maximum allowed length exceeded. At most {maxLength} characters is allowed.");
			}
		}
	}
}