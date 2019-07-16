为了方便从 [ILRuntime](https://github.com/Ourpalm/ILRuntime) 库获得更新，本仓库不包含ILRuntime 。

这里仅提供代码注入和运行时初始化功能，真正的运行时仍然是 ILRuntime，相关功能和特性可以查看 [ILRuntime介绍](https://ourpalm.github.io/ILRuntime/public/v1/guide/index.html)。



**使用前(必要操作)** 

1. 将本仓库的代码全部拷贝到你的项目等待编译完成。并从 [ILRuntime](https://github.com/Ourpalm/ILRuntime) 根目录中拷贝 ILRuntime、Mono.Cecil 两个文件夹到 **CsHotfix/ILSource/** 下。

   注：`ILRuntime/Runtime/CLRBinding/` 文件夹不要替换。

2. 执行菜单 `CsHotFix -> 插件 -> 清理ILRuntime多余文件` 删除ILRuntime多余文件。

**使用**

1. 开启热更，菜单栏  `CsHotFix -> 插件 -> 开启` 。
2. 对可能需要热更的类 打上 [CsHot] 标签。
3. 菜单栏`CsHotFix -> 一键生成`  生成相关的代码。

如果在编辑器下使用，需要修改



示例U3D工程和热更的C#工程，请查看 [CsHotfix_U3D]()



这个仓库主要为了方便自己在Unity中使用补丁式C#热更 以及更便捷的更新ILR代码，使用的Unity2018.3测试。
