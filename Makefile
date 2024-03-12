run:
	dotnet run --project EfCoreIsolationExample.Console new && \
	( dotnet run --project EfCoreIsolationExample.Console 1 4 \
	& dotnet run --project EfCoreIsolationExample.Console 1 5 \
	& dotnet run --project EfCoreIsolationExample.Console 2 6 \
	& dotnet run --project EfCoreIsolationExample.Console 2 7)
