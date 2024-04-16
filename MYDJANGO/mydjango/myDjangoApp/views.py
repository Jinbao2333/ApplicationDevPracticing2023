from django.contrib.auth import authenticate, logout
from django.shortcuts import redirect, render

# Create your views here.
from django.http import HttpResponse, JsonResponse

from django.contrib.auth.models import User
# from django.contrib.auth import authenticate, logout
from myDjangoApp.models import Question

# def hello_template(request):
#     context     ={}
#     context['hello']='Hello World!'
#     context['school']='ECNU'
#     context['pai']=3.1415927
#     return render(request, 'hello.html', context)

def home(request):
    return HttpResponse("HOME")

def hello(request):
    return HttpResponse("Hello World!")

def login(request):
    # 如果请求方式是get,就返回本页面，否则（post方式）就获取页面提交的内容
    # 和数据库的用户名/密码做对比，相同就登录，不同就报错
    if request.method == "GET":
        return render(request, 
                     'login.html', 
                      {"error_msg": ""}
        )
    elif request.method == 'POST':
		# 获取参数
        user_name = request.POST.get('username', '')
        pwd = request.POST.get('password', '')
		# 用户已存在
        if User.objects.filter(username=user_name):
			# 使用内置方法验证
            user = authenticate(username=user_name, password=pwd)
			# 验证通过
            if user:
				# 用户已激活
                if user.is_active:
                      # 登录成功跳转到我主页
                    return render(request,'main.html',{"username":user_name})
				# 未激活
                else:
                    return JsonResponse({
						'code': 200,
						'msg': '用户未激活'
					})
			# 验证失败
            else:
                return JsonResponse({
					'code': 403,
					'msg': '用户认证失败'
				})
                #登录不成功就返回本页面，并给出错误信息
                #return render(request, 'login.html', {"error_msg": "用户名或密码错误"})
                #return HttpResponse("用户名或密码错误")
                #return redirect('https://www.ecnu.edu.cn')
		#用户不存在
        else:
            return redirect('/reg')  

def question(request):
    if request.method == 'GET':
        name = request.POST.get('name')
        datas = Question.objects.all()
        print(datas)
        for da in datas:
            question_text = da.question_text
    return render(request, 'question.html', {"datas": datas})

def student_list(request):
    #student_queryset = models.Student.objects.all()
    #return render(request，"student.html"，{"student_queryset":student_queryset})
    return render(request,"student.html")

def reg(request):
    if request.method == 'GET':
        return render(
            request,
            'reg.html'
        )
    elif request.method == 'POST':
        # 获取参数
        user_name = request.POST.get('username', '')
        pwd = request.POST.get('password', '')
        repwd = request.POST.get('repassword', '')
        if(repwd!=pwd):
            return JsonResponse({
				'code': 200,
				'msg': 'user pwd wrong!'
			})
           
		# 用户已存在
        if User.objects.filter(username=user_name): 
            return JsonResponse({
				'code': 200,
				'msg': 'user alread exist!'
			})
		# 用户不存在
        else:
			# 使用User内置方法创建用户
            user = User.objects.create_user(
				username=user_name,
				password=pwd,
				email='123@qq.com',
				is_staff=1,
				is_active=1,
				is_superuser=0
			)
            # return JsonResponse({
			# 	'code': 200,
			# 	'msg': 'reg ok'
			# })
            return redirect('/login')
    else:
        return JsonResponse({
			'code': 403,
			'msg': 'forbbiden'
		})
    #return render(request, 'login.html')
def logout(request):
	logout(request)
	return redirect('/login')
def student_list(request):
    #student_queryset = models.Student.objects.all()
    #return render(request，"student.html"，{"student_queryset":student_queryset})
    return render(request,"student.html")