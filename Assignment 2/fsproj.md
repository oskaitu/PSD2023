        <FsLexToolExe>fsharp/fslex.dll</FsLexToolExe>
    <FsYaccToolExe>fsharp/fsyacc.dll</FsYaccToolExe>
    
    
    <FsYacc Include="Parser.fsy">
      <OtherFlags>--module Parser</OtherFlags>
    </FsYacc>
    <Compile Include="Parser.fsi" />
    <Compile Include="Parser.fs" /> 
