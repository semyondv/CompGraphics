#include "D:\Projects\CG\lab10\packages\soil.1.16.0\build\native\include\soil.h"
#include "GL/glew.h"
#include "GL/freeglut.h"
#include <iostream>

static int w = 0, h = 0;

GLuint floor_texture_id;

GLfloat dist_x = 0, dist_y = 0;
GLfloat angle = 0;

GLfloat machine_coord_x = 0, machine_coord_y = 0;
GLfloat machine_angle = 0;

GLfloat cam_dist = 20;
GLfloat ang_hor = 0, ang_vert = -60;

GLfloat no_light[] = {0, 0, 0, 1};
GLfloat light[] = {1, 1, 1, 0};

double cam_x = 0;
double cam_y = 0;
double cam_z = 0;

float amb[] = {0.8, 0.8, 0.8};
float dif[] = {0.2, 0.2, 0.2};

const double step = 1;

void loadTextures() {

    floor_texture_id = SOIL_load_OGL_texture("textures\\floor.jpg", SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID,
        SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
}

void init() {
    glClearColor(0, 0, 0, 1);

    glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);

    const GLfloat light_diffuse[] = {1.0, 1.0, 1.0, 1.0};
    const GLfloat light_specular[] = {1.0, 1.0, 1.0, 1.0};

    loadTextures();

    glLightfv(GL_LIGHT1, GL_DIFFUSE, light_diffuse);
    glLightfv(GL_LIGHT1, GL_SPECULAR, light_specular);
    glLightfv(GL_LIGHT2, GL_DIFFUSE, light_diffuse);
    glLightfv(GL_LIGHT2, GL_SPECULAR, light_specular);
    glLightfv(GL_LIGHT3, GL_DIFFUSE, light_diffuse);
    glLightfv(GL_LIGHT3, GL_SPECULAR, light_specular);
    glLightfv(GL_LIGHT4, GL_DIFFUSE, light_diffuse);

    glEnable(GL_DEPTH_TEST);
    glEnable(GL_COLOR_MATERIAL);
    glEnable(GL_LIGHTING);
    glEnable(GL_LIGHT0);
}

void drawFloor() {
    glEnable(GL_TEXTURE_2D);
    glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);
    glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, amb);
    glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, dif);
    glBindTexture(GL_TEXTURE_2D, floor_texture_id);

    glBegin(GL_QUADS);
    glTexCoord2f(0, 0); glNormal3f(0, 0, 1); glVertex3f(-20, -20, 0);
    glTexCoord2f(0, 1); glNormal3f(0, 0, 1); glVertex3f(-20, 20, 0);
    glTexCoord2f(1, 1); glNormal3f(0, 0, 1); glVertex3f(20, 20, 0);
    glTexCoord2f(1, 0); glNormal3f(0, 0, 1); glVertex3f(20, -20, 0);
    glEnd();

    glDisable(GL_TEXTURE_2D);
}

void drawLamps() {

    const GLfloat light_pos[] = {0.f, 0.f, 3.4f, 1.f};
    glColor3f(0.2f, 0.2f, 0.2f);
    glPushMatrix();
    glTranslatef(-4, -7, 0);
    glutSolidCylinder(0.07, 3, 10, 10);
    glPushMatrix();
    glColor3f(0.8f, 0.8f, 0.0f);
    glTranslatef(0, 0, 3.1);
    if (glIsEnabled(GL_LIGHT1))
        glMaterialfv(GL_FRONT, GL_EMISSION, light);
    else
        glMaterialfv(GL_FRONT, GL_EMISSION, no_light);
    glutSolidSphere(0.3, 10, 10);
    glPopMatrix();
    glLightfv(GL_LIGHT1, GL_POSITION, light_pos);
    glPopMatrix();

    glColor3f(0.2f, 0.2f, 0.2f);
    glPushMatrix();
    glTranslatef(-4, 7, 0);
    glutSolidCylinder(0.07, 3, 10, 10);
    glPushMatrix();
    glColor3f(0.8f, 0.8f, 0.0f);
    glTranslatef(0, 0, 3.1);
    if (glIsEnabled(GL_LIGHT2))
        glMaterialfv(GL_FRONT, GL_EMISSION, light);
    else
        glMaterialfv(GL_FRONT, GL_EMISSION, no_light);
    glutSolidSphere(0.3, 10, 10);
    glPopMatrix();
    glLightfv(GL_LIGHT2, GL_POSITION, light_pos);
    glPopMatrix();

    glColor3f(0.5f, 0.5f, 0.5f);
}

void drawWood() {
    glColor3f(0.1f, 0.0f, 0.0f);
    glPushMatrix();
    glTranslatef(-4, 0, 0);
    glutSolidCylinder(0.3, 1.1, 4, 4);
    glPopMatrix();

    glColor3f(0.1f, 0.8f, 0.1f);
    glPushMatrix();
    glTranslatef(-4, 0, 1.1);
    glutSolidCone(1.4f, 2.2f, 10, 10);
    glPopMatrix();

    glPushMatrix();
    glTranslatef(-4, 0, 2.9f);
    glutSolidCone(1.0f, 1.8f, 10, 10);
    glPopMatrix();

    glPushMatrix();
    glTranslatef(-4, 0, 4.3f);
    glutSolidCone(0.7f, 1.4f, 10, 10);
    glPopMatrix();

}
void drawCar() {
    glPushMatrix();
    const GLfloat light_pos[] = {1.f, 0.f, 0.f};
    GLfloat dir[] = {1, 0, 0, 1};
    GLfloat pos[] = {0,0,0};

    glTranslated(dist_x, dist_y, 1);
    glRotated(angle, 0, 0, 1);
    //Кузов
    glPushMatrix();
    glScaled(2, 1, 1);
    glColor3f(0.4f, 0.4f, 0.4f);
    glutSolidCube(1);

    glPopMatrix();
    //Конец кузова
    //Задние колеса
    glColor3f(0.15f, 0.15f, 0.15f);
    glPushMatrix();
    glTranslated(-0.3, -0.5, -0.6);
    glRotated(90, 1, 0, 0);
    glutSolidTorus(0.15, 0.2, 20, 20);
    glTranslated(0, 0, -1);
    glutSolidTorus(0.15, 0.2, 20, 20);
    glPopMatrix();
    //Конец задних колес
    //Кабина
    glPushMatrix();
    glTranslated(1.3, 0, -0.1);
    glColor3f(0.8f, 0.8f, 0.8f);
    glutSolidCube(0.8);
    //Фары
    glPushMatrix();
    if (glIsEnabled(GL_LIGHT3))
        glMaterialfv(GL_FRONT, GL_EMISSION, light);
    else
        glMaterialfv(GL_FRONT, GL_EMISSION, no_light);
    glTranslated(0.4, 0.3, 0);
    glColor3f(0.8f, 0.8f, 0.15f);
    glutSolidSphere(0.1, 20, 20);
    glMaterialfv(GL_FRONT, GL_EMISSION, no_light);
    glLightfv(GL_LIGHT3, GL_POSITION, pos);
    glLightf(GL_LIGHT3, GL_SPOT_CUTOFF, 60);
    glLightfv(GL_LIGHT3, GL_SPOT_DIRECTION, dir);
    glTranslated(0, -0.6, 0);
    if (glIsEnabled(GL_LIGHT4))
        glMaterialfv(GL_FRONT, GL_EMISSION, light);
    else
        glMaterialfv(GL_FRONT, GL_EMISSION, no_light);
    glutSolidSphere(0.1, 20, 20);
    glLightfv(GL_LIGHT4, GL_POSITION, pos);
    glLightf(GL_LIGHT4, GL_SPOT_CUTOFF, 60);
    glLightfv(GL_LIGHT4, GL_SPOT_DIRECTION, dir);
    glMaterialfv(GL_FRONT, GL_EMISSION, no_light);
    glPopMatrix();
    //Конец фар
    //Конец кабины
    //Передние колеса
    glColor3f(0.15f, 0.15f, 0.15f);
    glTranslated(0, -0.5, -0.5);
    glRotated(90, 1, 0, 0);
    glutSolidTorus(0.15, 0.2, 20, 20);
    glTranslated(0, 0, -1);
    glutSolidTorus(0.15, 0.2, 20, 20);
    glPopMatrix();
    //Конец передних колес
    glDisable(GL_TEXTURE_2D);
    glPopMatrix();
    glPopMatrix();
}

void update() {
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();

    double ang_vert_r = ang_vert / 180 * 3.1416;
    double ang_hor_r = ang_hor / 180 * 3.1416;
    cam_x = cam_dist * std::sin(ang_vert_r) * std::cos(ang_hor_r);
    cam_y = cam_dist * std::sin(ang_vert_r) * std::sin(ang_hor_r);
    cam_z = cam_dist * std::cos(ang_vert_r);

    gluLookAt(cam_x, cam_y, cam_z, 0., 0., 0., 0., 0., 1.);
    drawWood();
    drawLamps();
    drawFloor();
    drawCar();
    glFlush();
    glutSwapBuffers();
}

void updateCamera() {
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluPerspective(60.f, (float)w / h, 1.0f, 1000.f);
    glMatrixMode(GL_MODELVIEW);
}


void keyboard(unsigned char key, int x, int y) {
    switch (key) {
    case 'w':
        ang_vert += 5;
        break;
    case 's':
        ang_vert -= 5;
        break;
    case 'a':
        ang_hor -= 5;
        break;
    case 'd':
        ang_hor += 5;
        break;
    case 'q':
        cam_dist--;
        break;
    case 'z':
        cam_dist++;
        break;
    case '1':
        if (glIsEnabled(GL_LIGHT1))
            glDisable(GL_LIGHT1);
        else
            glEnable(GL_LIGHT1);
        break;
    case '2':
        if (glIsEnabled(GL_LIGHT2))
            glDisable(GL_LIGHT2);
        else
            glEnable(GL_LIGHT2);
        break;
    case '3':
        if (glIsEnabled(GL_LIGHT3))
            glDisable(GL_LIGHT3);
        else
            glEnable(GL_LIGHT3);
        break;
    case '4':
        if (glIsEnabled(GL_LIGHT4))
            glDisable(GL_LIGHT4);
        else
            glEnable(GL_LIGHT4);
        break;
    default:
        break;
    
    }
    glutPostRedisplay();
}

void reshape(int width, int height) {
    w = width;
    h = height;

    glViewport(0, 0, w, h);
    updateCamera();
}

void SpecialKeys(int key, int x, int y) {
    switch (key) {
    case GLUT_KEY_UP:
        dist_x += std::cos(angle / 180 * 3.1416) * 0.3;
        dist_y += std::sin(angle / 180 * 3.1416) * 0.3;
        break;
    case GLUT_KEY_DOWN:
        dist_x -= std::cos(angle / 180 * 3.1416) * 0.3;
        dist_y -= std::sin(angle / 180 * 3.1416) * 0.3;
        break;
    case GLUT_KEY_LEFT:
        angle -= 5;
        break;
    case GLUT_KEY_RIGHT:
        angle += 5;
        break;
    }
    glutPostRedisplay();
}

int main(int argc, char* argv[]) {
    glutInit(&argc, argv);
    glutInitWindowPosition(100, 100);
    glutInitWindowSize(800, 800);
    glutCreateWindow("texture and lighting");

    init();

    glutReshapeFunc(reshape);
    glutDisplayFunc(update);
    glutKeyboardFunc(keyboard);
    glutSpecialFunc(SpecialKeys);

    glutMainLoop();

    return 0;
}

