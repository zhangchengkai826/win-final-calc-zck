extern "C" {
	double __stdcall add(double a, double b) {
		return a + b;
	}

	double __stdcall sub(double a, double b) {
		return a - b;
	}

	double __stdcall mul(double a, double b) {
		return a * b;
	}

	double __stdcall div(double a, double b) {
		return a / b;
	}
}