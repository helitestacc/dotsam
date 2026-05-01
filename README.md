# .NET Lambda Symlink PoC

Demonstrates symlink injection in aws-lambda-builders dotnet workflow.

## Steps

```bash
git clone https://github.com/helitestacc/dotsam && cd dotsam
sam build
sam deploy --guided
```

Visit the Function URL to trigger exfiltration.
