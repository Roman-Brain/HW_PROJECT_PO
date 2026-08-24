using System;

namespace Parikmakherskaja.Core
{
    /// <summary>
    /// Требование ТЗ п.4: "простая процентная модель (сумма минус процент скидки),
    /// правила начисления скидок задаются администратором".
    /// </summary>
    public static class DiscountCalculator
    {
        public static decimal ApplyDiscount(decimal amount, decimal discountPercent)
        {
            if (discountPercent < 0 || discountPercent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercent), "Скидка должна быть в диапазоне 0..100%.");
            }

            return amount - amount * discountPercent / 100m;
        }
    }
}
