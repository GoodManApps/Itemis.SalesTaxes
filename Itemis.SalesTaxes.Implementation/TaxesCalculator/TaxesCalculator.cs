using Itemis.SalesTaxes.Abstraction.TaxesCalculator;

namespace Itemis.SalesTaxes.Implementation.TaxesCalculator
{
    /// <summary>
    /// Tax calculator.
    /// In terms of the pattern, the context of the 
    /// strategy pattern that represents the interface for clients.
    /// </summary>
    /// <typeparam name="TIn">Type of input before taxes</typeparam>
    /// <typeparam name="TOut">Type of output after taxes calculation</typeparam>
    public class TaxesCalculator<TIn, TOut>
    {
        /// <summary>
        /// Tax calculation strategy.
        /// </summary>
        private ITaxesStrategy<TIn, TOut> _strategy;

        /// <summary>
        /// Ctor with strategy
        /// </summary>
        /// <param name="strategy">Tax calculation strategy.</param>
        public TaxesCalculator(ITaxesStrategy<TIn, TOut> strategy)
        {
            _strategy = strategy;
        }

        /// <summary>
        /// Change strategy in runtime.
        /// </summary>
        /// <param name="strategy">Tax calculation strategy.</param>
        public void SetStrategy(ITaxesStrategy<TIn, TOut> strategy)
        {
            _strategy = strategy;
        }

        /// <summary>
        /// Выполнить расчёт наолга для входных данных.
        /// В терминах шаблона - делегируем некоторую работу объекту стратегии.
        /// </summary>
        /// <param name="taxeble">Input data before taxes</param>
        /// <returns>Output after taxes calculation</returns>
        public TOut CalculateTaxes(TIn taxeble)
        {
            var result = _strategy.EvaluateTaxes(taxeble);

            return result;
        }
    }
}
