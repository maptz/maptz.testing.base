using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Simplification;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace Maptz.Testing.Analyzers
{

    public static class DocumentExtensions
    {
        public static Document ApplyFix(this Document document, CodeAction codeAction)
        {
            var operations = codeAction.GetOperationsAsync(CancellationToken.None).Result;
            var solution = operations.OfType<ApplyChangesOperation>().Single().ChangedSolution;
            return solution.GetDocument(document.Id);
        }


        /// <summary>
        /// Get the existing compiler diagnostics on the inputted document.
        /// </summary>
        /// <param name="document">The Document to run the compiler diagnostic analyzers on</param>
        /// <returns>The compiler diagnostics that were found in the code</returns>
        public static IEnumerable<Diagnostic> GetCompilerDiagnostics(this Document document)
        {
            return document.GetSemanticModelAsync().Result.GetDiagnostics();
        }


        /// <summary>
        /// Given a document, turn it into a string based on the syntax root
        /// </summary>
        /// <param name="document">The Document to be converted to a string</param>
        /// <returns>A string containing the syntax of the Document after formatting</returns>
       public static string GetStringFromDocument(this Document document)
        {
            var simplifiedDoc = Simplifier.ReduceAsync(document, Simplifier.Annotation).Result;
            var root = simplifiedDoc.GetSyntaxRootAsync().Result;
            root = Formatter.Format(root, Formatter.Annotation, simplifiedDoc.Project.Solution.Workspace);
            return root.GetText().ToString();
        }
    }
}