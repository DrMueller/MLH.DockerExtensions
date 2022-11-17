# General

The solution in Mmu.Mlh.DockerExtensions is a generic one, open for different containers etc.

# Mmu.Mlh.DockerExtensions.DatabaseTests 

This projcet is self-contained and shows a solution to run entity-framework SQL tests in a container.
IMPORTANT: To run these tests in the CI, a special task, which starts the container, is in the CI Build, as we can't start Docker containers form the Azure DevOps Agent via code.
IMPORTANT2: For an application of Mmu.Mlh.DockerExtensions.DatabaseTests, see:
- CleanDddSimple
- TriDispo
