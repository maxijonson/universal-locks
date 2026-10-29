@echo off
if exist "servers\oxide-production\oxide\plugins" copy /Y "lite\UniversalLocks.cs" "servers\oxide-production\oxide\plugins\UniversalLocks.cs"
if exist "servers\oxide-staging\oxide\plugins" copy /Y "lite\UniversalLocks.cs" "servers\oxide-staging\oxide\plugins\UniversalLocks.cs"
if exist "servers\carbon-production\carbon\plugins" copy /Y "lite\UniversalLocks.cs" "servers\carbon-production\carbon\plugins\UniversalLocks.cs"
if exist "servers\carbon-staging\carbon\plugins" copy /Y "lite\UniversalLocks.cs" "servers\carbon-staging\carbon\plugins\UniversalLocks.cs"
