<div class="mermaid">
graph TD;
Test-->DotNetCore-Build;
Test-->DotNetCore-Test;
DotNetCore-Test-->Install-OpenCover;
Install-OpenCover-->Install-ReportGenerator;
DotNetCore-Build-->Clean;
DotNetCore-Build-->DotNetCore-Restore;
Clean-->Show-Info;
Clean-->Print-AppVeyor-Environment-Variables;
</div>
