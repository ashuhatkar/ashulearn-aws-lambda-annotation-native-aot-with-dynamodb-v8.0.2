build-GetProductsFunction:
    dotnet publish -c Release -r linux-arm64 ./GetProducts/GetProducts.csproj -o $(ARTIFACTS_DIR)

build-GetProductFunction:
    dotnet publish -c Release -r linux-arm64 ./GetProduct/GetProduct.csproj -o $(ARTIFACTS_DIR)

build-DeleteProductFunction:
    dotnet publish -c Release -r linux-arm64 ./DeleteProduct/DeleteProduct.csproj -o $(ARTIFACTS_DIR)

build-PutProductFunction:
    dotnet publish -c Release -r linux-arm64 ./PutProduct/PutProduct.csproj -o $(ARTIFACTS_DIR)
