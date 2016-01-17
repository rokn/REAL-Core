##Notes

###Creating a new project for running the core

####Add to the csproj
1. In the PropertyGroup tag :<br> 
    ```
    <MonoGamePlatform>Windows</MonoGamePlatform>
    <MonoGameContentBuilderExe>
    </MonoGameContentBuilderExe>
    ```
2. Below the item groups :<br> 
    ```
    <ItemGroup>
      <MonoGameContentReference Include="Content\Content.mgcb" />
    </ItemGroup>
    ```
3. At the end before closing tag :<br> 
    ``'
    <Import Project="$(MSBuildExtensionsPath)\MonoGame\v3.0\MonoGame.Content.Builder.targets" />
    ```
