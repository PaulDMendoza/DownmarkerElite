#!/bin/bash
# Generates the "VersionInfo.generated.cs" file.

downmarkerversion=`hg log -l 1 --template "{latesttag}.{latesttagdistance}.0"`
downmarkerid=`hg id`

sed "s/=version=/$downmarkerversion/" VersionInfo.template | sed "s/=id=/$downmarkerid/" > VersionInfo.generated.cs
echo done: Written VersionInfo.generated.cs
