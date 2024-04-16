from django.contrib.auth import authenticate, logout
from django.shortcuts import redirect, render

# Create your views here.
from django.http import HttpResponse, JsonResponse

from django.contrib.auth.models import User
from myDjangoApp.models import Question

def home(request):
    return HttpResponse("HOME")

def hello(request):
    return HttpResponse("Hello World!")

def login(request):
    if request.method == "GET":
        return render(request, 'login.html', {"error_msg": ""})
    elif request.method == 'POST':
        user_name = request.POST.get('username', '')
        pwd = request.POST.get('password', '')
        if User.objects.filter(username=user_name):
            user = authenticate(username=user_name, password=pwd)
            if user:
                if user.is_active:
                    # 登录成功，使用 redirect 函数重定向到 login_success 视图函数
                    return redirect('login_success', username=user_name)
                else:
                    return JsonResponse({
                        'code': 200,
                        'msg': '用户未激活'
                    })
            else:
                return JsonResponse({
                    'code': 403,
                    'msg': '用户认证失败'
                })
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
    return render(request, "student.html")

def login_success(request, username):
    return render(request, 'login_success.html', {"username": username})


def reg(request):
    if request.method == 'GET':
        return render(request, 'reg.html')
    elif request.method == 'POST':
        user_name = request.POST.get('username', '')
        pwd = request.POST.get('password', '')
        repwd = request.POST.get('repassword', '')
        if repwd != pwd:
            return JsonResponse({
                'code': 200,
                'msg': 'user pwd wrong!'
            })

        if User.objects.filter(username=user_name):
            return render(request, 'reg.html', {"error_msg": "用户已存在"})
        else:
            user = User.objects.create_user(
                username=user_name,
                password=pwd,
                email='123@qq.com',
                is_staff=1,
                is_active=1,
                is_superuser=0
            )
            return redirect('/login')
    else:
        return JsonResponse({
            'code': 403,
            'msg': 'forbbiden'
        })

def logout_view(request):
    logout(request)
    return redirect('/login')
