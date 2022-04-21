# General

The solution in Mmu.Mlh.DockerExtensions is a generic one, open for different containers etc.
The solution in Mmu.Mlh.DockerExtensions.DatabaseTests is self-contained and shows a solution to run entity-framework SQL tests in a container. IMPORTANT: The whole solution is in TriD, as it also needs setup on the CI (as we can't start a container form code in Azure DevOps Agents). Have look there for a complete solution.
