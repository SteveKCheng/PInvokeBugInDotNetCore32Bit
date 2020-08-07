#ifdef _WIN32
#define CALLCONV __cdecl
#define DLLEXPORT __declspec(dllexport)
#else
#define CALLCONV
#define DLLEXPORT
#endif

extern "C"
{
	struct ReturnStruct
	{
		void* a;
		void* b;
		void* c;
	};

	typedef ReturnStruct(CALLCONV *CallbackFn)(void* p);

	DLLEXPORT ReturnStruct CALLCONV InvokeCallback(CallbackFn fn, void* p)
	{
		return fn(p);
	}
}
