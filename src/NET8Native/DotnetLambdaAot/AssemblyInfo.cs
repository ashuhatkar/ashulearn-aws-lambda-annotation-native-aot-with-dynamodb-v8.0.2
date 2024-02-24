/*--****************************************************************************
  --* Project Name    : DotnetServerlessDemo
  --* Reference       : Amazon.Lambda.Annotations
  --*                   Amazon.Lambda.Core
  --*                   Amazon.Lambda.Serialization.SystemTextJson
  --* Description     : Project assembly configuration
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         09/11/24  CR-XXXXX Original
  --****************************************************************************/
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;

[assembly:LambdaGlobalProperties(GenerateMain = true)]
[assembly:LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<LambdaFunctionJsonSerializerContext>))]