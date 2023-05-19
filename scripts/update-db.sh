#!/usr/bin/env bash
dotnet ef database update --project Infrastructure --startup-project WebApi --context ApplicationDbContext
$SHELL
