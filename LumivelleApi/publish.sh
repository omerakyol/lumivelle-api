#!/bin/bash
# Publish Tulpar.AspCore.Templates to NuGet.org
# Usage: ./publish.sh <api-key>

set -e

if [ -z "$1" ]; then
  echo "Usage: $0 <nuget-api-key>"
  echo ""
  echo "Get your API key from: https://www.nuget.org/account/ApiKeys"
  echo "Create with scope 'Push new packages and package versions'"
  exit 1
fi

API_KEY="$1"
REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
NUPKG_DIR="${REPO_ROOT}/nupkg"
TEMPLATE_PACK="${REPO_ROOT}/template-pack"

echo "🔨 Building template package..."
mkdir -p "$NUPKG_DIR"
dotnet pack "$TEMPLATE_PACK/Tulpar.AspCore.Templates.csproj" -o "$NUPKG_DIR" -c Release

# Get the package version from the csproj
VERSION=$(grep -oP '<PackageVersion>\K[^<]+' "$TEMPLATE_PACK/Tulpar.AspCore.Templates.csproj")
PACKAGE="$NUPKG_DIR/Tulpar.AspCore.Templates.$VERSION.nupkg"

if [ ! -f "$PACKAGE" ]; then
  echo "❌ Package not found: $PACKAGE"
  exit 1
fi

echo "📦 Package: $PACKAGE"
echo "✨ Pushing to NuGet.org..."

dotnet nuget push "$PACKAGE" \
  -k "$API_KEY" \
  -s https://api.nuget.org/v3/index.json \
  --skip-duplicate

echo ""
echo "✅ Published successfully!"
echo ""
echo "Users can now install with:"
echo "  dotnet new install Tulpar.AspCore.Templates"
echo "  dotnet new tulpar-aspcore -n MyApi"
