extern "C"
{
	struct ReturnStruct
	{
		void* a;
		void* b;
		void* c;
	};

	typedef ReturnStruct(__cdecl *CallbackFn)(void* p);

	__declspec(dllexport) ReturnStruct __cdecl InvokeCallback(CallbackFn fn, void* p)
	{
		return fn(p);
	}
}
