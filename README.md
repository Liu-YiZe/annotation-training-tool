
### 软件介绍
- xclabel是一款开源支持多人协作的，样本导入+样本标注+模型训练+模型管理+模型测试+模型导出的工具
- 软件采用Python+Django开发，因此跨平台支持Windows/Linux/Mac

### 使用说明
- 首先安装Python和依赖库环境，推荐通过虚拟环境安装，可以参考下面的安装方法
- 环境安装完成后，启动服务： python manage.py runserver 0.0.0.0:9924
- 访问服务：在浏览器输入 http://127.0.0.1:9924 就可以开始了，默认账号 admin admin888


### Windows 通过虚拟环境安装依赖库
~~~
//建议Python3.10

//创建虚拟环境
python -m venv venv

//切换到虚拟环境
venv\Scripts\activate

//更新pip
python -m pip install --upgrade pip -i https://pypi.tuna.tsinghua.edu.cn/simple

//安装requirements
python -m pip install -r requirements.txt -i https://pypi.tuna.tsinghua.edu.cn/simple

~~~


### Linux/Mac 通过虚拟环境安装依赖库
~~~
//建议Python3.8

//创建虚拟环境
python -m venv venv

//切换到虚拟环境
source venv/bin/activate

//更新pip
python -m pip install --upgrade pip -i https://pypi.tuna.tsinghua.edu.cn/simple


//安装requirements
python -m pip install -r requirements-linux.txt -i https://pypi.tuna.tsinghua.edu.cn/simple

~~~


### Docker 部署
~~~
//进入到xclabel目录, 构建Docker镜像
docker build -t xclabel .

//在后台运行Docker容器，端口9924，将容器内的storage和log文件夹挂载到主机
docker run -d -p 9924:9924 -v ./storage:/xclabel/storage -v ./log:/xclabel/log xclabel

~~~
