# 基于microsoft/aspnetcore:2.0构建Docker Image
FROM microsoft/aspnetcore:2.0
 
# 设置工作路径
WORKDIR /app
 
# 将发布文件夹下的所有文件全部复制到工作目录
COPY ./bin/pub/*.* ./
 
ENV ASPNETCORE_URLS http://0.0.0.0:5000

# 执行dotnet GetMachNameWebAPI.dll命令
CMD ["dotnet", "GetMachNameWebAPI.dll"]