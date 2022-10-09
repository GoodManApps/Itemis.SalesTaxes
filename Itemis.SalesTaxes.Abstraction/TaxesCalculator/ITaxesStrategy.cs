namespace Itemis.SalesTaxes.Abstraction.TaxesCalculator
{
    /// <summary>
    /// Tax calculation strategy
    /// </summary>
    /// <typeparam name="TIn">The input parameter for which you want to calculate the tax</typeparam>
    /// <typeparam name="TOut">Output with calculated tax</typeparam>
    public interface ITaxesStrategy<TIn, TOut>
    {
        /// <summary>
        /// Realize the process to transform input data 
        /// to output format with tax calculation
        /// </summary>
        /// <param name="income">Incoming data</param>
        /// <returns></returns>
        TOut EvaluateTaxes(TIn income);
    }
}
