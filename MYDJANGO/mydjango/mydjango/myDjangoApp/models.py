from django.db import models
import django.utils.timezone as timezone
class Post(models.Model):
	# 字段定义
    title = models.CharField(max_length=200)
    slug = models.CharField(max_length=200)
    body = models.TextField()
    pub_date = models.DateTimeField(default=timezone.now)

	# 其他设置
    class Meta:
        ordering = ('-pub_date',)
	
    def __str__(self):
        return self.title

# Create your models here.

# Create your models here.
class User(models.Model):

    username = models.CharField(verbose_name='姓名', max_length=32)    
    studentno = models.CharField(verbose_name='学号', max_length=32)
    password = models.CharField(verbose_name='密码', max_length=32)


class Question(models.Model):
    question_text = models.CharField(max_length=220)  
    pub_date = models.DateTimeField(default=timezone.now)


class Choice(models.Model):
    question = models.ForeignKey(Question, on_delete=models.CASCADE)
    choice_text = models.CharField(max_length=200)
    votes = models.IntegerField(default=0)