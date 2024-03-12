run:
	dotnet run --project EfCoreIsolationExample.Console new && \
	( dotnet run --project EfCoreIsolationExample.Console 6 \
	& dotnet run --project EfCoreIsolationExample.Console 7)
