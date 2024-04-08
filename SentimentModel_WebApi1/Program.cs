﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using Microsoft.ML.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

// Configure app
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPredictionEnginePool<SentimentModel.ModelInput, SentimentModel.ModelOutput>()
    .FromFile("SentimentModel.mlnet", watchForChanges: true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Description = "Docs for my API", Version = "v1" });
});
var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Define prediction route & handler
app.MapPost("/predict",
    async (PredictionEnginePool<SentimentModel.ModelInput, SentimentModel.ModelOutput> predictionEnginePool, SentimentModel.ModelInput input) =>
        await Task.FromResult(predictionEnginePool.Predict(modelName: "SentimentAnalysisModel",input)));

// Run app
app.Run();


public class ModelInput
{
    public string SentimentText;
}

public class ModelOutput
{
    [ColumnName("PredictedLabel")]
    public bool Sentiment { get; set; }

    public float Probability { get; set; }

    public float Score { get; set; }
}