﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace AuditPredictor
{
    public partial class AuditModel1
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"Expected Duration (years)", @"Expected Duration (years)"),new InputOutputColumnPair(@"Actual Duration (years)", @"Actual Duration (years)"),new InputOutputColumnPair(@"Budget Amount", @"Budget Amount"),new InputOutputColumnPair(@"Actual Amount", @"Actual Amount"),new InputOutputColumnPair(@"Team Size", @"Team Size")})      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Project",outputColumnName:@"Project"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Business Unit",outputColumnName:@"Business Unit"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Expected Duration (years)",@"Actual Duration (years)",@"Budget Amount",@"Actual Amount",@"Team Size",@"Project",@"Business Unit"}))      
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName:@"Result",inputColumnName:@"Result"))      
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator:mlContext.BinaryClassification.Trainers.FastForest(new FastForestBinaryTrainer.Options(){NumberOfTrees=4,NumberOfLeaves=4,FeatureFraction=1F,LabelColumnName=@"Result",FeatureColumnName=@"Features"}),labelColumnName:@"Result"))      
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName:@"PredictedLabel",inputColumnName:@"PredictedLabel"));

            return pipeline;
        }
    }
}
