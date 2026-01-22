import time
from django.db import transaction
from django.http import HttpResponseRedirect, HttpResponse
from django.contrib.sessions.backends.base import UpdateError
from django.contrib.sessions.middleware import SessionMiddleware

try:
    from django.utils.deprecation import MiddlewareMixin  # Django 1.10.x
except ImportError:
    MiddlewareMixin = object  # Django 1.4.x - Django 1.9.x


class SimpleMiddleware(MiddlewareMixin):
    def process_request(self, request):
        # 移除所有登录重定向逻辑，允许所有请求通过
        return None

    def process_response(self, request, response):
        # print("process_response")
        return response

class ResponseSessionMiddleware(SessionMiddleware):
    def process_response(self, request, response):
        return super().process_response(request, response)