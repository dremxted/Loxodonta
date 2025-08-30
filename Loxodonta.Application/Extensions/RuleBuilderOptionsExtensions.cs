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
}