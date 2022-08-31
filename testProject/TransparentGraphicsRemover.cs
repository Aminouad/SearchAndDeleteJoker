//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace testProject
//{
//    public class TransparentGraphicsRemover : PdfContentStreamEditor
//    {
//        protected override void Write(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands)
//        {
//            String operatorString = oper.ToString();
//            if ("gs".Equals(operatorString))
//            {
//                updateTransparencyFrom((PdfName)operands[0]);
//            }

//            if (operatorMapping.Keys.Contains(operatorString))
//            {
//                // Downgrade the drawing operator if transparency is involved
//                // For details cf. the comment before the operatorMapping declaration
//                PdfLiteral[] mapping = operatorMapping[operatorString];

//                int index = 0;
//                if (strokingAlpha < 1)
//                    index |= 1;
//                if (nonStrokingAlpha < 1)
//                    index |= 2;

//                oper = mapping[index];
//                operands[operands.Count - 1] = oper;
//            }

//            base.Write(processor, oper, operands);
//        }

//        // The current transparency values; beware: save and restore state operations are ignored!
//        float strokingAlpha = 1;
//        float nonStrokingAlpha = 1;

//        void updateTransparencyFrom(PdfName gsName)
//        {
//            PdfDictionary extGState = getGraphicsStateDictionary(gsName);
//            if (extGState != null)
//            {
//                PdfNumber number = extGState.GetAsNumber(PdfName.ca);
//                if (number != null)
//                    nonStrokingAlpha = number.FloatValue;
//                number = extGState.GetAsNumber(PdfName.CA);
//                if (number != null)
//                    strokingAlpha = number.FloatValue;
//            }
//        }

//        PdfDictionary getGraphicsStateDictionary(PdfName gsName)
//        {
//            PdfDictionary extGStates = resources.GetAsDict(PdfName.EXTGSTATE);
//            return extGStates.GetAsDict(gsName);
//        }

//        //
//        // Map from an operator name to an array of operations it becomes depending
//        // on the current graphics state:
//        //
//        // * [0] the operation in case of no transparency
//        // * [1] the operation in case of stroking transparency
//        // * [2] the operation in case of non-stroking transparency
//        // * [3] the operation in case of stroking and non-stroking transparency
//        //
//        Dictionary<String, PdfLiteral[]> operatorMapping = new Dictionary<String, PdfLiteral[]>();

//        public TransparentGraphicsRemover()
//        {
//            PdfLiteral _S = new PdfLiteral("S");
//            PdfLiteral _s = new PdfLiteral("s");
//            PdfLiteral _f = new PdfLiteral("f");
//            PdfLiteral _fStar = new PdfLiteral("f*");
//            PdfLiteral _B = new PdfLiteral("B");
//            PdfLiteral _BStar = new PdfLiteral("B*");
//            PdfLiteral _b = new PdfLiteral("b");
//            PdfLiteral _bStar = new PdfLiteral("b*");
//            PdfLiteral _n = new PdfLiteral("n");

//            operatorMapping["S"] = new PdfLiteral[] { _S, _n, _S, _n };
//            operatorMapping["s"] = new PdfLiteral[] { _s, _n, _s, _n };
//            operatorMapping["f"] = new PdfLiteral[] { _f, _f, _n, _n };
//            operatorMapping["F"] = new PdfLiteral[] { _f, _f, _n, _n };
//            operatorMapping["f*"] = new PdfLiteral[] { _fStar, _fStar, _n, _n };
//            operatorMapping["B"] = new PdfLiteral[] { _B, _f, _S, _n };
//            operatorMapping["B*"] = new PdfLiteral[] { _BStar, _fStar, _S, _n };
//            operatorMapping["b"] = new PdfLiteral[] { _b, _f, _s, _n };
//            operatorMapping["b*"] = new PdfLiteral[] { _bStar, _fStar, _s, _n };
//        }
//    }
//}
