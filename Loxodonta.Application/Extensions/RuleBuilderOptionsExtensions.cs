using FluentValidation;

namespace Loxodonta.Application.Extensions;

public static class RuleBuilderOptionsExtensions
{
    public static IRuleBuilderOptions<T, IList<TElement>> MustContainUniqueValue<T, TElement, TKey>(
        this IRuleBuilder<T, IList<TElement>> ruleBuilder, 
        Func<TElement, TKey> keySelector)
    {
        return ruleBuilder.Must((_, list,context) =>
        {
            var originalCount = list.Count;
            var distinctCount = list.DistinctBy(keySelector).Count();

            if(originalCount != distinctCount)
            {

                return false;
            }
            return true;
        });
    }

    public static IRuleBuilderOptions<T,string> RequireLowercase<T>(
        this IRuleBuilder<T,string> ruleBuilder)
    {
        return ruleBuilder.Must(prop =>
        {
            return prop.Any(characher => char.IsLower(characher));
        });
    }

    public static IRuleBuilderOptions<T, string> RequireUppercase<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(prop =>
        {
            return prop.Any(characher => char.IsUpper(characher));
        });
    }

    public static IRuleBuilderOptions<T, string> RequireDigit<T>(
    this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(prop =>
        {
            return prop.Any(characher => char.IsNumber(characher));
        });
    }

    public static IRuleBuilderOptions<T, string> RequireNonAlphanumeric<T>(
    this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(prop =>
        {
            string nonAlphanumericProp = string.Concat(
                Array.FindAll(prop.ToCharArray(),
                char.IsLetterOrDigit));

            if(nonAlphanumericProp == prop)
            {
                return false;
            }
            return true;
        });
    }

    public static IRuleBuilderOptions<T, string> RequiredUniqueChars<T>(
    this IRuleBuilder<T, string> ruleBuilder, int requiredUniqueCharsNumber)
    {
        return ruleBuilder.Must(prop =>
        {
            IEnumerable<char> uniqueCharProp = prop.Distinct();
            return uniqueCharProp.Count() >= requiredUniqueCharsNumber;
        });
    }
}