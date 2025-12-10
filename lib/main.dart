import 'package:app_tracking_transparency/app_tracking_transparency.dart';
import 'package:break_out_water_challenge/core/screens/splash_screen.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'core/services/sdk_initializer.dart';
import 'firebase_options.dart';
import 'package:flutter/cupertino.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  initTrackingAppTransparency();
  await Firebase.initializeApp(options: DefaultFirebaseOptions.currentPlatform);
  SdkInitializer.prefs = await SharedPreferences.getInstance();
  await SdkInitializer.loadRuntimeStorageToDevice();
  var isFirstStart = !SdkInitializer.hasValue("isFirstStart");
  var isOrganic = SdkInitializer.getValue("Organic");
  print('add af2 $isFirstStart $isOrganic');
  if (isFirstStart) SdkInitializer.initAppsFlyer();
  FirebaseMessaging.onMessageOpenedApp.listen(_onMessageOpenedApp);
  runApp(const App());
}

void _onMessageOpenedApp(RemoteMessage message) {
  print('2 Notification caused the app to open: ${message.data.toString()}');
  SdkInitializer.pushURL = message.data['url'];
  // TODO: Add navigation or specific handling based on message data
}

Future<void> initTrackingAppTransparency() async {
  try {
    final TrackingStatus status =
        await AppTrackingTransparency.requestTrackingAuthorization();
    print('App Tracking Transparency status: $status');
    int timeout = 0;
    while (status == TrackingStatus.notDetermined && timeout < 10) {
      final TrackingStatus newStatus =
          await AppTrackingTransparency.requestTrackingAuthorization();
      await Future.delayed(const Duration(milliseconds: 200));
      timeout++;
    }
  } catch (e) {
    print('Error requesting App Tracking Transparency authorization: $e');
  }
}
class App extends StatelessWidget {
  const App({super.key});

  @override
  Widget build(BuildContext context) {
    return const CupertinoApp(
      debugShowCheckedModeBanner: false,
      home: SplashScreen(),
    );
  }
}

//SplashScreen
