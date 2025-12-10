
import 'package:flutter/cupertino.dart';

class AppConfig {
  static const String appsFlyerDevKey = 'TChPaBKwqSoi53H5KDpzRa';
  static const String appsFlyerAppId = '6756112629'; // Для iOS'
  static const String bundleId = 'com.rivendel.circleofmath'; // Для iOS'
  static const String locale = 'en'; // Для iOS'
  static const String os = 'iOS'; // Для iOS'
  static const String endpoint = 'https://circleofmath.com'; // Для iOS'
  static const String firebaseProjectId = 'circle-of-math'; // Для iOS'

//UI Settings
// Splash Screen
  static const Decoration splashDecoration = BoxDecoration(
    image: DecorationImage(
	
		// Указываем путь к изображению
		// Для изображений из сети: NetworkImage('URL')
		// Для локальных изображений: AssetImage('assets/image.jpg')
		image: AssetImage('assets/images/background.jpg'),
		
		// Режим заполнения - растягивает изображение на весь контейнер
		fit: BoxFit.cover,
		// Альтернативные варианты fit:
		// BoxFit.fill - растягивает с искажением пропорций
		// BoxFit.contain - сохраняет пропорции, может быть с полями
		// BoxFit.fitWidth - по ширине контейнера
		// BoxFit.fitHeight - по высоте контейнера
		// BoxFit.scaleDown - уменьшает если нужно, но не увеличивает
    //cover
		
		// Выравнивание изображения (если есть свободное пространство)
		alignment: Alignment.center,
		// Повторение изображения (если не заполняет полностью)
		// repeat: ImageRepeat.repeat, // повторять по обоим осям
		// repeat: ImageRepeat.repeatX, // повторять по горизонтали
		// repeat: ImageRepeat.repeatY, // повторять по вертикали
		
		// Цветовая фильтрация (можно наложить цвет поверх изображения)
		// colorFilter: ColorFilter.mode(
			// Colors.blue.withOpacity(0.3),
			// BlendMode.color,
		// ),
	)
  );

  static const Gradient splashGradient = LinearGradient(
    begin: Alignment.topCenter,
    end: Alignment.bottomCenter,
    colors: [
      Color(0xFF2AFFFF),
      Color(0xFF0D64AB),
    ],
  );
  static const Color loadingTextColor = Color(0xFFFFFFFF);
  static const Color spinerColor = Color(0xFCFFFFFF);
// Push Request Screen Settings

  static const Decoration pushRequestDecorationFade = BoxDecoration(
    image: DecorationImage(
	
		// Указываем путь к изображению
		// Для изображений из сети: NetworkImage('URL')
		// Для локальных изображений: AssetImage('assets/image.jpg')
		image: AssetImage('assets/images/fade.png'),
		
		// Режим заполнения - растягивает изображение на весь контейнер
		fit: BoxFit.cover,
		// Альтернативные варианты fit:
		// BoxFit.fill - растягивает с искажением пропорций
		// BoxFit.contain - сохраняет пропорции, может быть с полями
		// BoxFit.fitWidth - по ширине контейнера
		// BoxFit.fitHeight - по высоте контейнера
		// BoxFit.scaleDown - уменьшает если нужно, но не увеличивает
		
		// Выравнивание изображения (если есть свободное пространство)
		alignment: Alignment.center,
		// Повторение изображения (если не заполняет полностью)
		// repeat: ImageRepeat.repeat, // повторять по обоим осям
		// repeat: ImageRepeat.repeatX, // повторять по горизонтали
		// repeat: ImageRepeat.repeatY, // повторять по вертикали
		
		// Цветовая фильтрация (можно наложить цвет поверх изображения)
		// colorFilter: ColorFilter.mode(
			// Colors.blue.withOpacity(0.3),
			// BlendMode.color,
		// ),
	)
  );

  static const Decoration pushRequestDecoration = BoxDecoration(
    image: DecorationImage(
	
		// Указываем путь к изображению
		// Для изображений из сети: NetworkImage('URL')
		// Для локальных изображений: AssetImage('assets/image.jpg')
		image: AssetImage('assets/images/background.jpg'),
		
		// Режим заполнения - растягивает изображение на весь контейнер
		fit: BoxFit.cover,
		// Альтернативные варианты fit:
		// BoxFit.fill - растягивает с искажением пропорций
		// BoxFit.contain - сохраняет пропорции, может быть с полями
		// BoxFit.fitWidth - по ширине контейнера
		// BoxFit.fitHeight - по высоте контейнера
		// BoxFit.scaleDown - уменьшает если нужно, но не увеличивает
		
		// Выравнивание изображения (если есть свободное пространство)
		alignment: Alignment.center,
		// Повторение изображения (если не заполняет полностью)
		// repeat: ImageRepeat.repeat, // повторять по обоим осям
		// repeat: ImageRepeat.repeatX, // повторять по горизонтали
		// repeat: ImageRepeat.repeatY, // повторять по вертикали
		
		// Цветовая фильтрация (можно наложить цвет поверх изображения)
		// colorFilter: ColorFilter.mode(
			// Colors.blue.withOpacity(0.3),
			// BlendMode.color,
		// ),
	)
  );

  static const Gradient pushRequestGradient = LinearGradient(
    begin: Alignment.topLeft,
    end: Alignment.bottomRight,
    colors: [
      Color(0xFF2AFFFF),
      Color(0xFF0D64AB),
    ],
  );
  static const Gradient pushRequestFadeGradient = LinearGradient(
    begin: Alignment.topCenter,
    end: Alignment.bottomCenter,
    colors: [
      Color(0x00000000),
      Color.fromARGB(135, 0, 0, 0),
    ],
  );
  static const Color titleTextColor = Color(0xFFFFFFFF);
  static const Color subtitleTextColor = Color(0x80FDFDFD);

  static const Color yesButtonColor = Color(0xFFFAE29D);
  static const Color yesButtonShadowColor = Color.fromARGB(255, 245, 174, 112);
  static const Color yesButtonTextColor = Color.fromARGB(255, 0, 0, 0);
  static const Color skipTextColor = Color.fromARGB(255, 231, 229, 229);

  // Путь к логотипу, если не находит добавить в pubspec.yaml
  static const String logoPath = 'assets/images/Logo.png';

  // экран ошибки подключения интернета
  // Splash Screen
  static const Decoration errorScreenDecoration = BoxDecoration(
    gradient: AppConfig.errorScreenGradient,
  );

  static const Gradient errorScreenGradient = LinearGradient(
    begin: Alignment.topCenter,
    end: Alignment.bottomCenter,
    colors: [
      Color.fromARGB(255, 119, 75, 17),
      Color.fromARGB(255, 227, 154, 7),
    ],
  );
  static const Color errorScreenTextColor = Color(0xFFFFFFFF);
  static const Color errorScreenIconColor = Color(0xFCFFFFFF);

// экран загрузки WebGL
  static String webGLEndpoint =
      'https://play.unity.com/api/v1/games/game/d5903747-7972-40c2-8d44-a737d658257f/build/latest/frame'; //'https://play.unity.com/en/games/1712c4c6-d525-479c-9aa7-303fbb78c940/robbies-coins';

  static const Decoration webGLLoadingDecoration = BoxDecoration(
    gradient: AppConfig.splashGradient,
  );
  static const String webGLLoadingLogoPath = 'assets/images/Logo.png';
  static const Gradient webGLLoadingGradient = LinearGradient(
    begin: Alignment.topCenter,
    end: Alignment.bottomCenter,
    colors: [
      Color(0xFF30225C),
      Color(0xFF150B34),
    ],
  );
  static const Color webGLLoadingTextColor = Color(0xFFFFFFFF);
  static const Color webGLSpinerColor = Color(0xFCFFFFFF);
}
