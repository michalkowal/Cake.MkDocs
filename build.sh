#!/usr/bin/env bash

# Define default arguments.
SCRIPT="setup.cake"
TARGET="Default"
CONFIGURATION="Release"
VERBOSITY="Verbose"
EXPERIMENTAL=false
WHATIF=false
MONO=false
SKIPTOOLPACKAGERESTORE=false
SCRIPT_ARGUMENTS=()

# Parse arguments.
for i in "$@"; do
    case $1 in
		-s|--script) SCRIPT="$2"; shift;;
        -t|--target) TARGET="$2"; shift ;;
        -c|--configuration) CONFIGURATION="$2"; shift ;;
        -v|--verbosity) VERBOSITY="$2"; shift ;;
		-e|--experimental) EXPERIMENTAL=true ;;
		-wi|--whatif) WHATIF=true ;;
		-m|--mono) MONO=true ;;
		-sr|--skiptoolpackagerestore) SKIPTOOLPACKAGERESTORE=true ;;
        --) shift; SCRIPT_ARGUMENTS+=("$@"); break ;;
        *) SCRIPT_ARGUMENTS+=("$1") ;;
    esac
    shift
done

echo "Preparing to run build script..."

# Define directories.
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
TOOLS_DIR=$SCRIPT_DIR/tools
NUGET_EXE=$TOOLS_DIR/nuget.exe
CAKE_EXE=$TOOLS_DIR/Cake/Cake.exe
NUGET_URL=https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
PACKAGES_CONFIG=$TOOLS_DIR/packages.config
PACKAGES_CONFIG_MD5=$TOOLS_DIR/packages.config.md5sum

# Should we use mono?
USE_MONO=""
if [ "$MONO" = true ]; then
	echo "Using the Mono based scripting engine."
	USE_MONO="-mono"
fi

# Should we use the new Roslyn?
USE_EXPERIMENTAL=""
if [ "$EXPERIMENTAL" = true ]; then
	echo "Using experimental version of Roslyn."
	USE_EXPERIMENTAL="-experimental"
fi

# Should we use the new Roslyn?
USE_DRYRUN=""
if [ "$WHATIF" = true ]; then
	USE_DRYRUN="-dryrun"
fi

# Make sure the tools folder exist.
if [ ! -d "$TOOLS_DIR" ]; then
	echo "Creating tools directory..."
	mkdir "$TOOLS_DIR"
fi

# Make sure that packages.config exist.
if [ ! -f "$PACKAGES_CONFIG" ]; then
    echo "Downloading packages.config..."
    curl -Lsfo "$PACKAGES_CONFIG" "http://cakebuild.net/download/bootstrapper/packages"
    if [ $? -ne 0 ]; then
        echo "Could not download packages.config."
        exit 1
    fi
fi

# TODO: Try find NuGet.exe in path if not exists

# Download NuGet if it does not exist.
if [ ! -f "$NUGET_EXE" ]; then
    echo "Downloading NuGet..."
    curl -Lsfo "$NUGET_EXE" $NUGET_URL
    if [ $? -ne 0 ]; then
        echo "Could not download NuGet.exe."
        exit 1
    fi
fi

# Save nuget.exe path to environment to be available to child processed
export NUGET_EXE

# Restore tools from Nuget?
if [ "$SKIPTOOLPACKAGERESTORE" = false ]; then
	pushd $TOOLS_DIR
	
	MD5HASH=($(md5sum "$PACKAGES_CONFIG"| cut -d ' ' -f 1))
	# TODO: CHECK MD5
	
	echo "Restoring tools from NuGet..."
	chmod +x "$NUGET_EXE"
	NUGETOUTPUT=$(mono "$NUGET_EXE" install -ExcludeVersion -PreRelease -OutputDirectory "$TOOLS_DIR" -Source https://www.myget.org/F/cake/api/v3/index.json)
	
	if [ $? -ne 0 ]; then
        echo "An error occured while restoring NuGet tools."
        exit 1
    fi
	
	echo "$NUGETOUTPUT"
	popd
fi

# Make sure that Cake has been installed.
if [ ! -f "$CAKE_EXE" ]; then
    echo "Could not find Cake.exe at '$CAKE_EXE'."
    exit 1
fi

# Start Cake
echo "Running build script..."
chmod +x "$CAKE_EXE"
exec mono "$CAKE_EXE" $SCRIPT --target=$TARGET --configuration=$CONFIGURATION --verbosity=$VERBOSITY $USE_MONO $USE_DRYRUN $USE_EXPERIMENTAL "${SCRIPT_ARGUMENTS[@]}"
